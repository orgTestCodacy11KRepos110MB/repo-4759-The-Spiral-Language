﻿module Spiral.PartEval

open Spiral.Tokenize
open Spiral.Parsing
open Spiral.Prepass
open Spiral.Utils
open System.Collections.Generic
open System

type Env<'a,'b> = {type' : StackSize * 'a []; value : StackSize * 'b []}

type LayoutType =
    | LayoutStack
    | LayoutHeap
    | LayoutHeapMutable

type Tag = int
type [<CustomComparison;CustomEquality>] T<'a,'b when 'a: equality and 'a: comparison> = 
    | T of 'a * 'b

    override a.Equals(b) =
        match b with
        | :? T<'a,'b> as b -> match a,b with T(a,_), T(b,_) -> a = b
        | _ -> false
    override a.GetHashCode() = match a with T(a,_) -> hash a
    interface IComparable with
        member a.CompareTo(b) = 
            match b with
            | :? T<'a,'b> as b -> match a,b with T(a,_), T(b,_) -> compare a b
            | _ -> raise <| ArgumentException "Invalid comparison for T."

type Ty =
    | PairT of Ty * Ty
    | KeywordT of KeywordTag * Ty []
    | FunctionT of Expr * StackSize * Ty [] * StackSize * Ty [] * is_forall : bool
    | TypeFunctionT of Expr * StackSize * Ty []
    | RecordT of Map<KeywordTag, Ty>
    | PrimT of PrimitiveType
    
    | LayoutT of LayoutType * RData
    | ArrayT of Ty
    | RuntimeFunctionT of Ty * Ty
    | MacroT of RData

and Data =
    | TyPair of Data * Data
    | TyKeyword of KeywordTag * Data []
    | TyFunction of Expr * StackSize * Ty [] * StackSize * Data [] * is_forall : bool
    | TyRecord of Map<KeywordTag, Data>
    | TyLit of Value

    | TyV of TyV
    | TyR of int // For use in join points, layout types and macros
and TyV = T<Tag,Ty>

and RData = R of Data // has TyRef // TODO: Hash cons this.

type Trace = ParserCombinators.PosKey list
type JoinPointKey = Expr * Ty []
type JoinPointType =
    | JoinPointClosure
    | JoinPointMethod
type JoinPoint = JoinPointKey * JoinPointType * TyV []

type TypedBind =
    | TyLet of Data * Trace * TypedOp
    | TyLocalReturnOp of Trace * TypedOp
    | TyLocalReturnData of Data * Trace

and TypedOp = 
    | TyOp of Op * Data []
    | TyIf of cond: Data * tr: TypedBind [] * fl: TypedBind []
    | TyWhile of cond: JoinPoint * TypedBind []
    | TyCase of Data * (Data * TypedBind []) []
    | TyLayoutToNone of Data
    | TyJoinPoint of JoinPoint
    | TySetMutableRecord of Data * (Tag * Ty) [] * TyV []

/// Unlike v0.1 and previously, the functions can now have cycles so that needs to be taken care of during memoization.
let data_to_rdata' call_data =
    let m = Dictionary(HashIdentity.Reference)
    let call_args = ResizeArray()
    let rec f x =
        match m.TryGetValue x with
        | true, v -> v
        | _ ->
            let memo r = m.Add(x,TyR m.Count); r
            match x with
            | TyPair(a,b) -> memo <| TyPair(f a, f b)
            | TyKeyword(a,b) -> memo <| TyKeyword(a, Array.map f b)
            | TyFunction(a,b,c,d,e,z) -> m.Add(x,TyR m.Count); TyFunction(a,b,c,d,Array.map f e,z)
            | TyRecord l -> memo <| TyRecord(Map.map (fun _ -> f) l)
            | TyV(T(_,ty) as t) -> memo (call_args.Add t; TyV(T(call_args.Count-1, ty)))
            | TyLit _ -> x
            | TyR _ -> failwith "Compiler error"
    let x = f call_data
    call_args.ToArray(),R x

let data_to_rdata call_data = data_to_rdata' call_data |> snd // TODO: Specialize this.

let rdata_to_data' i (R call_data) =
    let m = Dictionary(HashIdentity.Structural)
    let r_args = ResizeArray()
    let rec f x =
        let memo r = m.Add(m.Count,r); r
        match x with
        | TyPair(a,b) -> memo <| TyPair(f a, f b)
        | TyKeyword(a,b) -> memo <| TyKeyword(a, Array.map f b)
        | TyFunction(a,b,c,d,e,z) -> 
            let e' = Array.zeroCreate<_> e.Length
            let r = TyFunction(a,b,c,d,e',z)
            m.Add(m.Count,r)
            Array.iteri (fun i x -> e'.[i] <- f x) e
            r
        | TyRecord l -> memo <| TyRecord(Map.map (fun _ -> f) l)
        | TyV(T(_,ty) as t) -> memo (r_args.Add t; let r = TyV(T(!i, ty)) in i := !i+1; r)
        | TyLit _ -> x
        | TyR x -> m.[x]
    let x = f call_data
    r_args.ToArray(),x

let rdata_to_data i x = rdata_to_data' i x |> snd // TODO: Specialize this.

let data_free_vars call_data =
    let m = HashSet(HashIdentity.Reference)
    let free_vars = ResizeArray()
    let rec f x =
        if m.Add x then
            match x with
            | TyPair(a,b) -> f a; f b
            | TyKeyword(a,b) -> Array.iter f b
            | TyFunction(a,b,c,d,e,z) -> Array.iter f e
            | TyRecord l -> Map.iter (fun _ -> f) l
            | TyV(T(_,ty) as t) -> free_vars.Add t
            | TyLit _ | TyR _ -> ()
    f call_data
    free_vars.ToArray()

let rdata_free_vars (R x) = data_free_vars x

let ty_to_data i x =
    let m = Dictionary(HashIdentity.Reference)
    let rec f x = 
        match x with
        | PairT(a,b) -> TyPair(f a, f b) 
        | KeywordT(a,b) -> TyKeyword(a,Array.map f b)
        | FunctionT(a,b,c,d,e,z) -> 
            match m.TryGetValue x with
            | true, v -> v
            | _ ->
                let e' = Array.zeroCreate e.Length
                let r = TyFunction(a,b,c,d,e',z)
                m.Add(x,r)
                Array.iteri (fun i x -> e'.[i] <- f x) e
                m.Remove(x) |> ignore // Non-nested mapping should not share vars
                r
        | RecordT l -> TyRecord(Map.map (fun k -> f) l)
        | PrimT _ | LayoutT _ | ArrayT _ | RuntimeFunctionT _ | MacroT _ -> let r = TyV(T(!i,x)) in i := !i+1; r
        | TypeFunctionT _ -> failwith "Compiler error: Cannot turn a type function to a runtime variable."
    f x

let value_to_ty = function
    | LitUInt8 _ -> PrimT UInt8T
    | LitUInt16 _ -> PrimT UInt16T
    | LitUInt32 _ -> PrimT UInt32T
    | LitUInt64 _ -> PrimT UInt64T
    | LitInt8 _ -> PrimT Int8T
    | LitInt16 _ -> PrimT Int16T
    | LitInt32 _ -> PrimT Int32T
    | LitInt64 _ -> PrimT Int64T
    | LitFloat32 _ -> PrimT Float32T
    | LitFloat64 _ -> PrimT Float64T   
    | LitBool _ -> PrimT BoolT
    | LitString _ -> PrimT StringT
    | LitChar _ -> PrimT CharT

let data_to_ty x =
    let m = Dictionary(HashIdentity.Reference)
    let rec f x =
        let memoize f = memoize m f x
        let memoize_rec e ret f = memoize_rec m e ret f x
        match x with
        | TyPair(a,b) -> memoize (fun _ -> PairT(f a, f b))
        | TyKeyword(a,b) -> memoize (fun _ -> KeywordT(a, Array.map f b))
        | TyFunction(a,b,c,d,e,z) -> memoize_rec e (fun e' -> FunctionT(a,b,c,d,e',z)) f
        | TyRecord l -> memoize (fun _ -> RecordT(Map.map (fun _ -> f) l))
        | TyV(T(_,ty) as t) -> ty
        | TyLit x -> value_to_ty x
        | TyR _ -> failwith "Compiler error"
    f x

type LangEnv = {
    trace : Trace
    seq : ResizeArray<TypedBind>
    cse : Dictionary<Op * Data [], Data> list
    i : VarTag ref
    env_global_type : Ty []
    env_global_value : Data []
    env_stack_type : Ty []
    env_stack_type_ptr : int
    env_stack_value : Data []
    env_stack_value_ptr : int 
    }

let push_value_var x (d: LangEnv) =
    d.env_stack_value.[d.env_stack_value_ptr] <- x
    {d with env_stack_value_ptr=d.env_stack_value_ptr+1}

let push_type_var x (d: LangEnv) =
    d.env_stack_type.[d.env_stack_type_ptr] <- x
    {d with env_stack_type_ptr=d.env_stack_type_ptr+1}

let seq_apply (d: LangEnv) end_dat =
    let inline end_ () = d.seq.Add(TyLocalReturnData(end_dat,d.trace))
    if d.seq.Count > 0 then
        match d.seq.[d.seq.Count-1] with
        | TyLet(end_dat',a,b) when Object.ReferenceEquals(end_dat,end_dat') -> d.seq.[d.seq.Count-1] <- TyLocalReturnOp(a,b)
        | _ -> end_()
    else end_()
    d.seq.ToArray()

let cse_tryfind (d: LangEnv) key =
    d.cse |> List.tryPick (fun x ->
        match x.TryGetValue key with
        | true, v -> Some v
        | _ -> None
        )

let cse_add (d: LangEnv) k v = (List.head d.cse).Add(k,v)

let layout_to_none (d: LangEnv) = function
    | TyV(T(_,LayoutT(t,l))) as v ->
        let x = rdata_to_data d.i l 
        d.seq.Add(TyLet(x,d.trace,TyLayoutToNone(v)))
        x
    | a -> a

let layout_to_some layout (d: LangEnv) = function
    | TyV(T(_,LayoutT(t,l))) as x when t = layout -> x
    | x ->
        let x = layout_to_none d x
        let consed_data = data_to_rdata x
        let ret_ty = LayoutT(layout,consed_data)
        let ret = ty_to_data d.i ret_ty
        let layout =
            match layout with
            | LayoutStack -> LayoutToStack
            | LayoutHeap -> LayoutToHeap
            | LayoutHeapMutable -> LayoutToHeapMutable
        d.seq.Add(TyLet(ret,d.trace,TyOp(layout,[|x|])))
        ret

let push_typedop d op ret_ty =
    let ret = ty_to_data d.i ret_ty
    d.seq.Add(TyLet(ret,d.trace,op))
    ret

let push_op_no_rewrite (d: LangEnv) op l ret_ty = push_typedop d (TyOp(op,l)) ret_ty
let push_binop_no_rewrite d op (a,b) ret_ty = push_op_no_rewrite d op ([|a;b|]) ret_ty
let push_triop_no_rewrite d op (a,b,c) ret_ty = push_op_no_rewrite d op ([|a;b;c|]) ret_ty

let push_op (d: LangEnv) op l ret_ty =
    let key = op,l
    match cse_tryfind d key with
    | Some x -> x
    | None ->
        let ret = ty_to_data d.i ret_ty
        d.seq.Add(TyLet(ret,d.trace,TyOp(op,l)))
        cse_add d key ret
        ret

let push_binop d op (a,b) ret_ty = push_op d op ([|a;b|]) ret_ty
let push_triop d op (a,b,c) ret_ty = push_op d op ([|a;b;c|]) ret_ty

let rec partial_eval (d: LangEnv) x = 
    let inline ev d x = partial_eval d x
    let inline ev2 d a b = ev d a, ev d b
    let inline ev3 d a b c = ev d a, ev d b, ev d c
    let inline ev_seq d x =
        let d = {d with seq=ResizeArray(); i=ref !d.i}
        let x = ev d x
        let x_ty = data_to_ty x
        seq_apply d x, x_ty
    let inline ev_annot d x = ev_seq {d with cse=Dictionary(HashIdentity.Structural) :: d.cse} x |> snd
    
    let inline push_var_ptr ptr x = d.env_stack_value.[ptr] <- x; ptr+1
    let inline v x = if x < 0 then d.env_global_value.[-x-1] else d.env_stack_value.[x]
    failwith ""
