﻿module Spiral.Typechecking
open Spiral.Config
type TT =
    | KindStar
    | KindFun of TT * TT
    | KindMetavar of int

type T =
    | TyB
    | TyPrim of PrimitiveType
    | TySymbol of string
    | TyPair of T * T
    | TyRecord of Map<string, T>
    | TyFun of T * T
    | TyForall of (string * TT) * T
    | TyArray of T
    | TyHigherOrder of int * TT
    | TyApp of T * T * TT // Regular type functions (TyInl) get reduced, while this represents the staged reduction of nominals.
    | TyInl of (string * TT) * T
    | TyVar of string * TT // Staged type vars. Should only appear in TyInl's body. 
    | TyMetavar of int * TT

type VariableBoundError =
    | UnboundVar
    | ModuleUnbound
    | ModuleShadowed
    | ModuleShadowedAndUnbound
    | ModuleIndexFailed

type TypeError =
    | KindError of TT * TT
    | RecordKeyNotFound of string
    | ExpectedSymbolAsRecordKey of T
    | UnboundVariable
    | UnboundModule
    | ModuleIndexFailedInOpen
    | ExpectedSymbolAsUnionKey
    | DuplicateKeyInUnion
    | TermError of T * T
    | ForallVarScopeError of string * T * T
    | ForallMetavarScopeError of string * T * T
    | MetavarsNotAllowedInRecordWith
    | ExpectedRecord
    | ExpectedRecordAsResultOfIndex of string
    | RecordIndexFailed of string
    | ExpectedSymbolInRecordWith

open Spiral.BlockParsing
exception TypeErrorException of (Range * TypeError) list

let rec tt = function
    | TyHigherOrder(_,x) | TyApp(_,_,x) | TyMetavar(_,x) | TyVar(_,x) -> x
    | TyB | TyPrim _ | TyForall _ | TyFun _ | TyRecord _ | TyPair _ | TySymbol _ | TyArray _ -> KindStar
    | TyInl((_,k),a) -> KindFun(k,tt a)

let rec typevar = function
    | RawKindStar -> KindStar
    | RawKindFun(a,b) -> KindFun(typevar a, typevar b)
let typevars (l : TypeVar list) = List.map (fun (_,(a,b)) -> a, typevar b) l

let rec metavars = function
    | RawTVar _ | RawTTerm _ | RawTPrim _ | RawTWildcard _ | RawTB _ | RawTSymbol _ -> Set.empty
    | RawTMetaVar(_,a) -> Set.singleton a
    | RawTForall(_,(_,(_,_)),a) | RawTArray(_,a) -> metavars a
    | RawTPair(_,a,b) | RawTApply(_,a,b) | RawTFun(_,a,b) -> metavars a + metavars b
    | RawTRecord(_,l) -> Map.fold (fun s _ v -> s + metavars v) Set.empty l

type Env = { ty : Map<string,T>; term : Map<string,T> }
let module_open (top_env : Env) (r : Range) b l =
    let tryFind env x =
        match Map.tryFind x env.term, Map.tryFind x env.ty with
        | Some (TyRecord a), Some (TyRecord b) -> ValueSome {term=a; ty=b}
        | _ -> ValueNone
    match tryFind top_env b with
    | ValueNone -> Error(r, UnboundModule)
    | ValueSome env ->
        let rec loop env = function
            | (r,x) :: x' ->
                match tryFind env x with
                | ValueSome env -> loop env x'
                | _ -> Error(r, ModuleIndexFailedInOpen)
            | [] -> Ok env
        loop env l

let assert_bound_vars' (top_env : Env) term ty x =
    let errors = ResizeArray()
    let rec cterm term ty x =
        let check (a,b) = if Set.contains b term = false && Map.containsKey b top_env.term = false then errors.Add(a,UnboundVariable)
        match x with
        | RawSymbolCreate _ | RawDefaultLit _ | RawLit _ | RawB _ -> ()
        | RawV(a,b) -> check (a,b)
        | RawType(_,x) -> ctype term ty x
        | RawMatch(_,body,l) -> cterm term ty body; List.iter (fun (a,b) -> cterm (cpattern term ty a) ty b) l
        | RawFun(_,l) -> List.iter (fun (a,b) -> cterm (cpattern term ty a) ty b) l
        | RawForall(_,(_,(a,_)),b) -> cterm term (Set.add a ty) b
        | RawRecBlock(_,l,on_succ) -> 
            let term = List.fold (fun s ((_,x),_) -> Set.add x s) term l
            List.iter (fun (_,x) -> cterm term ty x) l
            cterm term ty on_succ
        | RawRecordWith(_,a,b,c) ->
            List.iter (cterm term ty) a
            List.iter (function
                | RawRecordWithSymbol(_,e) | RawRecordWithSymbolModify(_,e) -> cterm term ty e
                | RawRecordWithInjectVar(v,e) | RawRecordWithInjectVarModify(v,e) -> check v; cterm term ty e
                ) b
            List.iter (function RawRecordWithoutSymbol _ -> () | RawRecordWithoutInjectVar (a,b) -> check (a,b)) c
        | RawOp(_,_,l) -> List.iter (cterm term ty) l
        | RawReal(_,x) | RawJoinPoint(_,x) -> cterm term ty x
        | RawAnnot(_,a,b) -> cterm term ty a; ctype term ty b
        | RawTypecase(_,a,b) -> 
            ctype term ty a
            List.iter (fun (a,b) -> 
                ctype term ty a
                cterm term (ty + metavars a) b
                ) b
        | RawModuleOpen(_,(a,b),l,on_succ) ->
            match module_open top_env a b l with
            | Ok x ->
                let combine e m = Map.fold (fun s k _ -> Set.add k s) e m
                cterm (combine term x.term) (combine ty x.ty) on_succ
            | Error e -> errors.Add(e)
        | RawSeq(_,a,b) | RawPairCreate(_,a,b) | RawIfThen(_,a,b) | RawApply(_,a,b) -> cterm term ty a; cterm term ty b
        | RawIfThenElse(_,a,b,c) -> cterm term ty a; cterm term ty b; cterm term ty c
    and ctype term ty x =
        match x with
        | RawTPrim _ | RawTWildcard _ | RawTB _ | RawTSymbol _ | RawTMetaVar _ -> ()
        | RawTVar(a,b) -> if Set.contains b ty = false && Map.containsKey b top_env.ty = false then errors.Add(a,UnboundVariable)
        | RawTPair(_,a,b) | RawTApply(_,a,b) | RawTFun(_,a,b) -> ctype term ty a; ctype term ty b
        | RawTRecord(_,l) -> Map.iter (fun _ -> ctype term ty) l
        | RawTForall(_,(_,(a,_)),b) -> ctype term (Set.add a ty) b
        | RawTArray(_,a) -> ctype term ty a
        | RawTTerm (_,a) -> cterm term ty a
    and cpattern term' ty x =
        let rec loop term x = 
            let f = loop term
            match x with
            | PatDefaultValue _ | PatValue _ | PatSymbol _ | PatB _ | PatE _ -> term
            | PatVar(_,b) -> Set.add b term
            | PatDyn(_,x) | PatUnbox(_,x) -> f x
            | PatPair(_,a,b) -> loop (loop term a) b
            | PatRecordMembers(_,l) ->
                List.fold (fun s -> function
                    | PatRecordMembersSymbol(_,x) -> loop s x
                    | PatRecordMembersInjectVar((a,b),x) ->
                        if Set.contains b term = false && Map.containsKey b top_env.term = false then errors.Add(a,UnboundVariable)
                        loop s x
                    ) term l
            | PatActive(_,a,b) -> cterm (term' + term) ty a; f b
            | PatAnd(_,a,b) | PatOr(_,a,b) -> loop (loop term a) b
            | PatAnnot(_,a,b) -> let r = f a in ctype (term' + r) ty b; r // TODO: I am doing it like this so I can reuse this code later for variable highting.
            | PatWhen(_,a,b) -> let r = f a in cterm (term' + r) ty b; r
            | PatNominal(_,a,b) -> (if Map.containsKey (snd a) top_env.ty = false then errors.Add(fst a,UnboundVariable)); f b
        term' + loop Set.empty x

    match x with
    | Choice1Of2 x -> cterm term ty x
    | Choice2Of2 x -> ctype term ty x
    errors
let assert_bound_vars (top_env : Env) term ty x =
    let errors = assert_bound_vars' top_env term ty x
    if 0 < errors.Count then raise (TypeErrorException (Seq.toList errors))

let rec subst (m : Map<string,T>) x =
    let f = subst m
    let fun_body a b = subst (Map.add (fst a) (TyVar a) m) b
    match x with
    | TyMetavar _ | TyHigherOrder _ | TyB | TyPrim _ | TySymbol _ -> x
    | TyPair(a,b) -> TyPair(f a, f b)
    | TyRecord l -> TyRecord(Map.map (fun _ -> f) l)
    | TyFun(a,b) -> TyFun(f a, f b)
    | TyArray a -> TyArray(f a)
    | TyApp(a,b,c) -> TyApp(f a, f b, c)
    | TyVar(a,_) -> m.[a]
    | TyForall(a,b) -> TyForall(a, fun_body a b)
    | TyInl(a,b) -> TyInl(a, fun_body a b)

open Spiral.Tokenize

let infer (top_env : Env) (env : Env) x =
    let errors = ResizeArray()
    let term' = ResizeArray()
    let kind = ResizeArray()
    let fresh_kind () = let x = KindMetavar kind.Count in kind.Add(x); x
    let fresh_var' x = let x = TyMetavar(term'.Count,x) in term'.Add(x); x
    let fresh_var () = fresh_var' KindStar

    let rec kind_get i =
        match kind.[i] with
        | KindMetavar i' when i <> i' -> let x = kind_get i' in kind.[i] <- x; x
        | x -> x

    let rec kind_subst = function
        | KindMetavar i -> kind_get i
        | KindFun(a,b) -> KindFun(kind_subst a,kind_subst b)
        | KindStar -> KindStar

    let rec term_get i =
        match term'.[i] with
        | TyMetavar(i',_) when i <> i' -> let x = term_get i' in term'.[i] <- x; x
        | x -> x

    let rec term_subst = function
        | TyVar _ | TyHigherOrder _ | TyB | TyPrim _ | TySymbol _ as x -> x
        | TyPair(a,b) -> TyPair(term_subst a, term_subst b)
        | TyRecord l -> TyRecord(Map.map (fun _ -> term_subst) l)
        | TyFun(a,b) -> TyFun(term_subst a, term_subst b)
        | TyForall(a,b) -> TyForall(a,term_subst b)
        | TyArray a -> TyArray(term_subst a)
        | TyApp(a,b,c) -> TyApp(term_subst a, term_subst b, c)
        | TyMetavar(i,_) -> term_get i
        | TyInl _ -> failwith "Compiler error: TyInl should have been substituted away by this point."

    let forall_subst_all x =
        let rec loop (m : Map<string,T>) = function
            | TyForall((n,k),b) ->
                let v = TyMetavar(term'.Count,k)
                term'.Add(v)
                let m = Map.add n x m
                loop m b
            | x -> subst m x
        loop Map.empty x

    let forall_subst_single ((n,k),b) =
        let v = TyMetavar(term'.Count,k)
        term'.Add(v)
        let m = Map.add n v Map.empty
        subst m b

    let unify_kind r (got : TT) (expected : TT) : unit =
        let rec loop = function
            | KindStar, KindStar -> ()
            | KindFun(a,a'), KindFun(b,b') -> loop (a,b); loop (a',b')
            | KindMetavar a & a', KindMetavar b & b' -> if a < b then kind.[b] <- a' else kind.[a] <- b'
            | KindMetavar a, b | b, KindMetavar a -> kind.[a] <- b
            | _ -> raise (TypeErrorException [r, KindError (got, expected)])
        loop (kind_subst got, kind_subst expected)
    let unify (r : Range) (got : T) (expected : T) : unit =
        let er () = raise (TypeErrorException [r, TermError(got, expected)])
        let rec occurs_check i = 
            let f = occurs_check i
            function
            | TyHigherOrder _ | TyB | TyVar _ | TyPrim _ | TySymbol _ -> ()
            | TyForall (_,a) | TyArray a -> f a
            | TyApp(a,b,_) | TyFun(a,b) | TyPair(a,b) -> f a; f b
            | TyRecord l -> Map.iter (fun _ -> f) l
            | TyInl _ -> failwith "Compiler error: TyInl should not be here."
            | TyMetavar (i',_) -> if i = i' then er()
        let rec loop = function
            | TyVar(a,_), TyVar(b,_) when a <> b -> er ()
            | TyMetavar(a,ka), TyMetavar(b,kb) ->
                unify_kind r ka kb
                if a < b then term'.[b] <- got else term'.[a] <- expected
            | (TyMetavar(a,ka), b | b, TyMetavar(a,ka)) ->
                occurs_check a b
                unify_kind r ka (tt b)
                term'.[a] <- b
            | (TyPair(a,b), TyPair(a',b') | TyFun(a,a'), TyFun(b,b') | TyApp(a,a',_), TyApp(b,b',_)) -> loop (a,b); loop (a',b')
            | TyRecord l, TyRecord l' ->
                let a,b = Map.toArray l, Map.toArray l'
                if a.Length <> b.Length then er ()
                else Array.iter2 (fun (_,a) (_,b) -> loop (a,b)) a b
            | TyHigherOrder(i,_), TyHigherOrder(i',_) -> if i <> i' then er()
            | TyB, TyB -> er()
            | TyPrim x, TyPrim x' -> if x <> x' then er()
            | TySymbol x, TySymbol x' -> if x <> x' then er()
            | TyArray a, TyArray b -> loop (a,b)
            | (TyForall(a,b), TyForall(a',b')) ->
                unify_kind r (snd a) (snd a')
                loop (forall_subst_single(a,b), forall_subst_single(a',b'))
            | (TyInl _, _ | _, TyInl _) -> failwith "Compiler error: TyInl should have been substituted away by this time."
            | _ -> er ()

        try loop (term_subst got, term_subst expected)
        with :? TypeErrorException as e -> errors.AddRange e.Data0

    let rec apply_record r s l x =
        let f = apply_record r s
        match x with
        | TySymbol x ->
            match Map.tryFind x l with
            | Some x -> unify r s x
            | None -> errors.Add(r,RecordIndexFailed x)
        | TyPair(TySymbol x, b) ->
            match Map.tryFind x l with
            | Some (TyRecord l) -> f l b
            | Some a -> unify r a (TyFun(b,s))
            | None -> errors.Add(r,RecordIndexFailed x)
        | x -> errors.Add(r,ExpectedSymbolAsRecordKey x)
           
    let assert_bound_vars env a =
        let keys_of m = Map.fold (fun s k _ -> Set.add k s) Set.empty m
        assert_bound_vars' top_env (keys_of env.term) (keys_of env.ty) (Choice1Of2 a) |> errors.AddRange

    let rec term (env : Env) s x =
        let f = term env
        let f' x = let v = fresh_var() in f v x; term_subst v
        let v x = Map.tryFind x env.term |> Option.orElseWith (fun () -> Map.tryFind x top_env.term)
        match x with
        | RawB r -> unify r s TyB
        | RawV(r,x) ->
            match v x with
            | Some x -> unify r s (forall_subst_all x)
            | None -> errors.Add(r, UnboundVariable)
        | RawLit(r,x) ->
            match x with
            | LitUInt8 _ -> TyPrim UInt8T
            | LitUInt16 _ -> TyPrim UInt16T
            | LitUInt32 _ -> TyPrim UInt32T
            | LitUInt64 _ -> TyPrim UInt64T
            | LitInt8 _ -> TyPrim Int8T
            | LitInt16 _ -> TyPrim Int16T
            | LitInt32 _ -> TyPrim Int32T
            | LitInt64 _ -> TyPrim Int64T
            | LitFloat32 _ -> TyPrim Float32T
            | LitFloat64 _ -> TyPrim Float64T
            | LitBool _ -> TyPrim BoolT
            | LitString _ -> TyPrim StringT
            | LitChar _ -> TyPrim CharT
            |> unify r s
        | RawDefaultLit _ -> ()
        | RawSymbolCreate (r,x) -> unify r s (TySymbol x)
        | RawType(_,x) -> ty env s x
        | RawIfThenElse(_,cond,tr,fl) -> f (TyPrim BoolT) cond; f s tr; f s fl
        | RawIfThen(_,cond,tr) -> f (TyPrim BoolT) cond; f s tr
        | RawPairCreate(r,a,b) -> 
            let q,w = fresh_var(), fresh_var()
            unify r s (TyPair(q, w))
            f q a; f w b
        | RawSeq(_,a,b) -> f TyB a; f s b
        | RawReal(_,a) -> assert_bound_vars env a
        | RawOp(_,_,l) -> List.iter (assert_bound_vars env) l
        | RawJoinPoint(_,a) -> f s a
        | RawApply(r,a',b) -> 
            match f' a' with
            | TyRecord l -> apply_record r s l (f' b)
            | a -> let v = fresh_var() in unify (range_of_expr a') a (TyFun(v,s)); f v b
        | RawAnnot(_,a,b) -> ty env s b; f s a
        | RawForall _ -> failwith "Compiler error: Should be taken care of in the let statements."
        | RawModuleOpen(_,(a,b),l,on_succ) ->
            match module_open top_env a b l with
            | Ok x ->
                let combine e m = Map.foldBack Map.add m e
                term {term = combine env.term x.term; ty = combine env.ty x.ty} s on_succ
            | Error e -> errors.Add(e)
        | RawRecordWith(r,l,withs,withouts) ->
            let i = errors.Count
            let record x =
                match f' x with
                | TyRecord m -> m
                | TyMetavar _ -> raise (TypeErrorException (if errors.Count = i then [range_of_expr x, MetavarsNotAllowedInRecordWith] else []))
                | _ -> raise (TypeErrorException [range_of_expr x, ExpectedRecord])
            let symbol x =
                match f' x with
                | TySymbol x -> x
                | TyMetavar _ -> raise (TypeErrorException (if errors.Count = i then [range_of_expr x, MetavarsNotAllowedInRecordWith] else []))
                | _ -> raise (TypeErrorException [range_of_expr x, ExpectedSymbolInRecordWith])
            let tc (l,m) =
                let m =
                    List.fold (fun m x -> 
                        let with_symbol ((_,a),b) = 
                            let v = fresh_var()
                            f v b
                            Map.add a v m
                        let with_symbol_modify ((r,a),b) =
                            let x = Map.tryFind a m |> Option.defaultWith (fun () -> errors.Add(r,RecordIndexFailed a); fresh_var())
                            let v = fresh_var()
                            f (TyFun(x,v)) b
                            Map.add a v m
                        let inline with_injext next ((r,a),b) =
                            match v a |> Option.map term_subst with
                            | Some (TySymbol a) -> next ((r,a),b)
                            | Some x -> errors.Add(r, ExpectedSymbolAsRecordKey x); m
                            | None -> errors.Add(r, UnboundVariable); m
                        match x with
                        | RawRecordWithSymbol(a,b) -> with_symbol (a,b)
                        | RawRecordWithSymbolModify(a,b) -> with_symbol_modify (a,b)
                        | RawRecordWithInjectVar(a,b) -> with_injext with_symbol (a,b)
                        | RawRecordWithInjectVarModify(a,b) -> with_injext with_symbol_modify (a,b)
                        ) m withs
                let m =
                    List.fold (fun m -> function
                        | RawRecordWithoutSymbol(_,a) -> Map.remove a m
                        | RawRecordWithoutInjectVar(_,a) ->
                            match v a |> Option.map term_subst with
                            | Some (TySymbol a) -> Map.remove a m
                            | Some x -> errors.Add(r, ExpectedSymbolAsRecordKey x); m
                            | None -> errors.Add(r, UnboundVariable); m
                        ) m withouts
                    |> TyRecord |> List.foldBack (fun (m,a) m' -> Map.add a m' m |> TyRecord) l
                if i = errors.Count then unify r s m
            try match l with
                | x :: x' ->
                    List.mapFold (fun m x ->
                        let sym = symbol x
                        match Map.tryFind sym m with
                        | Some (TyRecord m') -> (m,sym), m'
                        | Some _ -> raise (TypeErrorException [range_of_expr x, ExpectedRecordAsResultOfIndex sym])
                        | None -> raise (TypeErrorException [range_of_expr x, RecordIndexFailed sym])
                        ) (record x) x'
                | [] -> [], Map.empty
            with :? TypeErrorException as e -> errors.AddRange e.Data0; [], Map.empty
            |> tc
        //| RawMatch of Range * body: RawExpr * (Pattern * RawExpr) list
        //| RawFun of Range * (Pattern * RawExpr) list
        //| RawRecBlock of Range * ((Range * VarString) * RawExpr) list * on_succ: RawExpr // The bodies of a block must be RawInl or RawForall.
        | RawTypecase _ -> failwith "Compiler error: `typecase` should not appear in the top down segment."
        | _ -> failwith "TODO"
    and ty (env : Env) s x =
        let f s x = ty env s x
        let v x = Map.tryFind x env.ty |> Option.orElseWith (fun () -> Map.tryFind x top_env.ty)
        match x with
        | RawTWildcard _ -> ()
        | RawTB r -> unify r s TyB
        | RawTSymbol(r,x) -> unify r s (TySymbol x)
        | RawTPrim(r,x) -> unify r s (TyPrim x)
        | RawTArray(r,x) -> let v = fresh_var() in unify r s (TyArray v); f v x
        | RawTVar(r,n) -> 
            match v n with
            | Some x -> unify r s x
            | None -> errors.Add(r, UnboundVariable)
        | RawTPair(r,a,b) -> 
            let q,w = fresh_var(), fresh_var()
            unify r s (TyPair(q,w))
            f q a; f w b
        | RawTFun(r,a,b) -> 
            let q,w = fresh_var(), fresh_var()
            unify r s (TyFun(q,w))
            f q a; f w b
        | RawTRecord(r,l) -> 
            let l' = Map.map (fun _ _ -> fresh_var()) l
            unify r s (TyRecord l')
            Map.iter (fun k s -> f s l.[k]) l'
        | RawTForall(r,(_,(a,k)),b) ->
            let k = typevar k
            let x = fresh_var()
            unify r s (TyForall((a,k), x))
            ty {env with ty=Map.add a (TyVar(a,k)) env.ty} x b
        | RawTApply(r,a',b) ->
            let f' k x = let v = fresh_var' k in f v x; term_subst v
            match f' (fresh_kind()) a' with
            | TyRecord l -> apply_record r s l (f' KindStar b)
            | TyInl((name,k),body) -> let v = fresh_var' k in f v b; unify r s (subst (Map.add name v Map.empty) body)
            | a -> 
                let q,w = fresh_kind(), fresh_kind()
                unify_kind (range_of_texpr a') (tt a) (KindFun(q,w))
                let x = fresh_var' q
                f x b
                unify r s (TyApp(a,x,w))
        | RawTTerm(r,a) -> assert_bound_vars env a; unify r s (TySymbol "<term>")
        | RawTMetaVar _ -> failwith "Compiler error: This particular metavar is only for typecase's clauses. This happens during the bottom-up segment."
    let v = fresh_var()
    match x with
    | Choice1Of2 x -> term env v x
    | Choice2Of2 x -> ty env v x
    if 0 < errors.Count then raise (TypeErrorException (Seq.toList errors))
    else term_subst v

let add_var s (k,v) = Map.add k (TyVar (k,v)) s
let add_vars ty vars = List.fold add_var ty vars
let set_vars vars = List.map fst vars |> Set
let top_type (name : VarString, vars : TypeVar list, expr : RawTExpr) (top_env : Env) =
    let vars = typevars vars
    let body =
        assert_bound_vars top_env Set.empty (set_vars vars) (Choice2Of2 expr)
        infer top_env {term=Map.empty; ty=add_vars Map.empty vars} (Choice2Of2 expr)
    let inl = List.foldBack (fun x s -> TyInl(x,s)) vars body
    {top_env with ty = Map.add name inl top_env.ty}

type TopHigherOrder =
    | HOUnion of name: string * id: int * (string * TT) list * RawTExpr list
    | HONominal of name: string * id: int * (string * TT) list * RawTExpr

type HigherOrderCases =
    | HOCUnion of (string * TT) list * Map<string,T>
    | HOCNominal of (string * TT) list * T

let top_higher_order (l : TopHigherOrder list) hoc (top_env : Env) =
    let top_env = 
        List.fold (fun s (HOUnion(name,i,vars,_) | HONominal(name,i,vars,_)) ->
            let tt = List.foldBack (fun (_,x) s -> KindFun(x,s)) vars KindStar
            Map.add name (TyHigherOrder(i,tt)) s
            ) top_env.ty l
        |> fun ty -> {top_env with ty=ty}
    let errors = ResizeArray()
    let hoc =
        List.fold (fun hoc x ->
            match x with
            | HOUnion(_,i,vars,l) ->
                List.fold (fun cases expr ->
                    try assert_bound_vars top_env Set.empty (set_vars vars) (Choice2Of2 expr)
                        match infer top_env {term=Map.empty; ty=add_vars Map.empty vars} (Choice2Of2 expr) with
                        | TyPair(TySymbol x, b) -> 
                            if Map.containsKey x cases then errors.Add(range_of_texpr expr, DuplicateKeyInUnion); cases
                            else Map.add x b cases
                        | _ -> errors.Add (range_of_texpr expr, ExpectedSymbolAsUnionKey); cases
                    with :? TypeErrorException as x -> errors.AddRange(x.Data0); cases
                    ) Map.empty l
                |> fun l -> Map.add i (HOCUnion(vars,l)) hoc
            | HONominal(_,i,vars,expr) ->
                try assert_bound_vars top_env Set.empty (set_vars vars) (Choice2Of2 expr)
                    Map.add i (HOCNominal(vars, infer top_env {term=Map.empty; ty=add_vars Map.empty vars} (Choice2Of2 expr))) hoc
                with :? TypeErrorException as x -> errors.AddRange(x.Data0); hoc
            ) hoc l
    if 0 < errors.Count then raise (TypeErrorException (errors |> Seq.toList))
    hoc, top_env

let top_prototype (name,a,vars,expr) (top_env : Env) =
    let tt = List.foldBack (fun (_,x) s -> KindFun(x,s)) vars KindStar
    let l = (a,tt) :: vars
    assert_bound_vars top_env Set.empty (set_vars vars) (Choice2Of2 expr)
    let body = 
        infer top_env {term=Map.empty; ty=add_vars Map.empty vars} (Choice2Of2 expr)
        |> List.foldBack (fun a b -> TyForall(a,b)) l
    {top_env with term = Map.add name body top_env.term}

open Spiral.Blockize
let tc (l : Bundle list) = 
    ()