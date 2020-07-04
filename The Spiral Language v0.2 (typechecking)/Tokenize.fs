﻿module Spiral.Tokenize
open System
open System.Text
open Spiral.LineParsers
open Spiral.ParserCombinators

type TokenSpecial =
    | SpecIn
    | SpecAnd
    | SpecFun
    | SpecMatch
    | SpecTypecase
    | SpecFunction
    | SpecWith
    | SpecWithout
    | SpecAs
    | SpecWhen
    | SpecInl
    | SpecForall
    | SpecLet
    | SpecInm
    | SpecInb
    | SpecRec
    | SpecIf
    | SpecThen
    | SpecElif
    | SpecElse
    | SpecJoin
    | SpecType
    | SpecNominal
    | SpecReal
    | SpecUnion
    | SpecOpen
    | SpecWildcard

type BracketState = Open | Close
type Bracket = Round | Square | Curly

type Literal = 
    | LitUInt8 of uint8
    | LitUInt16 of uint16
    | LitUInt32 of uint32
    | LitUInt64 of uint64
    | LitInt8 of int8
    | LitInt16 of int16
    | LitInt32 of int32
    | LitInt64 of int64
    | LitFloat32 of float32
    | LitFloat64 of float
    | LitBool of bool
    | LitString of string
    | LitChar of char

type SpiralToken =
    | TokVar of string
    | TokSymbol of string
    | TokSymbolPaired of string
    | TokValue of Literal
    | TokDefaultValue of string
    | TokOperator of string
    | TokUnaryOperator of string
    | TokComment of string
    | TokKeyword of TokenSpecial
    | TokBracket of Bracket * BracketState

let token_groups = function
    | TokVar _ -> 0 // variable
    | TokSymbol _ | TokSymbolPaired _ -> 1 // symbol
    | TokValue(LitString _) -> 2 // string
    | TokValue _ | TokDefaultValue -> 3 // number
    | TokOperator _ -> 4 // operator
    | TokUnaryOperator _ -> 5 // unary operator
    | TokComment _ -> 6 // comment
    | TokKeyword _ -> 7 // keyword
    | TokBracket _ -> 8 // bracket

let is_small_var_char_starting c = Char.IsLower c || c = '_'
let is_var_char c = Char.IsLetterOrDigit c || c = '_' || c = '''
let is_big_var_char_starting c = Char.IsUpper c
let is_var_char_starting c = Char.IsLetter c || c = '_'
let is_parenth_open c = 
    let f x = c = x
    f '(' || f '[' || f '{'
let is_parenth_close c = 
    let f x = c = x
    f ')' || f ']' || f '}'

// http://www.asciitable.com/
let is_operator_char c =
    let f x = c = x
    '!' <= c && c <= '~' && (is_var_char c || f '"' || is_parenth_open c || is_parenth_close c) = false
let is_prefix_separator_char c = 
    let f x = c = x
    f ' ' || f eol || is_parenth_open c
let is_postfix_separator_char c = 
    let f x = c = x
    f ' ' || f eol || is_parenth_close c
let is_separator_char c = is_prefix_separator_char c || is_parenth_close c

let var (s: Tokenizer) = 
    let from = s.from
    let ok x = ({from=from; nearTo=s.from}, x)
    let body x = 
        skip ':' s (fun () -> TokSymbolPaired(x) |> ok)
            (fun () ->
                let f x = TokKeyword(x)
                match x with
                | "in" -> f SpecIn
                | "and" -> f SpecAnd | "fun" -> f SpecFun
                | "match" -> f SpecMatch | "typecase" -> f SpecTypecase
                | "function" -> f SpecFunction
                | "with" -> f SpecWith | "without" -> f SpecWithout
                | "as" -> f SpecAs | "when" -> f SpecWhen
                | "inl" -> f SpecInl | "forall" -> f SpecForall
                | "let" -> f SpecLet | "inm" -> f SpecInm
                | "inb" -> f SpecInb | "rec" -> f SpecRec
                | "if" -> f SpecIf | "then" -> f SpecThen
                | "elif" -> f SpecElif | "else" -> f SpecElse
                | "join" -> f SpecJoin | "type" -> f SpecType 
                | "nominal" -> f SpecNominal | "real" -> f SpecReal
                | "union" -> f SpecUnion
                | "open" -> f SpecOpen | "_" -> f SpecWildcard
                | "true" -> TokValue(LitBool true) | "false" -> TokValue(LitBool false)
                | x -> TokVar(x)
                |> ok
                )

    (many1Satisfy2L is_var_char_starting is_var_char "variable" |>> body .>> spaces) s

let number (s: Tokenizer) = 
    let from = s.from
    let ok x = ({from=from; nearTo=s.from}, x) |> Ok

    let parser (s: Tokenizer) = 
        if peek s = '-' && Char.IsDigit (peek' s 1) && is_prefix_separator_char (peek' s -1) then 
            inc s
            number_fractional s |> Result.map (function 
                | (a,Some b) -> sprintf "-%s.%s" a b
                | (a,None) -> "-"+a)
        else number_fractional s |> Result.map (function 
                | (a,Some b) -> sprintf "%s.%s" a b
                | (a,None) -> a)
    
    let followedBySuffix x (s: Tokenizer) =
        let inline safe_parse string_to_val val_to_lit val_dsc =
            if is_separator_char (peek s) then 
                match string_to_val x with
                | true, x -> val_to_lit x |> TokValue |> ok
                | false, _ -> Error [{from=from; nearTo=s.from}, Message (sprintf "The string %s cannot be safely parsed as %s." x val_dsc)]
            else error_char s.from (Expected "separator")
        let skip c = skip c s (fun () -> true) (fun () -> false)
        if skip 'i' then
            if skip '8' then safe_parse SByte.TryParse LitInt8 "int8"
            elif skip '1' && skip '6' then safe_parse Int16.TryParse LitInt16 "int16"
            elif skip '3' && skip '2' then safe_parse Int32.TryParse LitInt32 "int32"
            elif skip '6' && skip '4' then safe_parse Int64.TryParse LitInt64 "int64"
            else error_char s.from (Expected "8,16,32 or 64")
        elif skip 'u' then
            if skip '8' then safe_parse Byte.TryParse LitUInt8 "uint8"
            elif skip '1' && skip '6' then safe_parse UInt16.TryParse LitUInt16 "uint16"
            elif skip '3' && skip '2' then safe_parse UInt32.TryParse LitUInt32 "uint32"
            elif skip '6' && skip '4' then safe_parse UInt64.TryParse LitUInt64 "uint64"
            else error_char s.from (Expected "8,16,32 or 64")
        elif skip 'f' then
            if skip '3' && skip '2' then safe_parse Single.TryParse LitFloat32 "float32"
            elif skip '6' && skip '4' then safe_parse Double.TryParse LitFloat64 "float64"
            else error_char s.from (Expected "32 or 64")
        else TokDefaultValue x |> ok

    (parser >>= followedBySuffix .>> spaces) s

let symbol s =
    let from = s.from
    let f x = ({from=from; nearTo=s.from}, TokSymbol x)

    let x = peek s
    let x' = peek' s 1
    if x = '.' then
        if x' = '(' then inc' 2 s; ((many1SatisfyL is_operator_char "operator") .>> skip_char ')' |>> f .>> spaces) s
        else inc s; ((many1Satisfy2L is_var_char_starting is_var_char "variable") |>> f .>> spaces) s
    else error_char from (Expected "symbol")

let comment (s : Tokenizer) =
    let from = s.from
    let x = peek s
    let x' = peek' s 1
    if x = '/' && x' = '/' then 
        inc' 2 s
        let com = s.text.[s.from..]
        s.from <- s.text.Length
        Ok ({from=from; nearTo=s.from}, TokComment com)
    else
        error_char from (Expected "comment")

let operator (s : Tokenizer) = 
    let from = s.from
    let ok x = ({from=from; nearTo=s.from}, x) |> Ok
    let is_separator_prev = is_prefix_separator_char (peek' s -1)
    let f name (s: Tokenizer) = 
        if is_separator_prev && (is_postfix_separator_char (peek s) = false) then TokUnaryOperator(name) |> ok
        else TokOperator(name) |> ok
    (many1SatisfyL is_operator_char "operator"  >>= f .>> spaces) s

let string_raw s =
    let from = s.from
    let f x = {from=from; nearTo=s.from}, TokValue(LitString x)
    (skip_string "@\"" >>. chars_till_string "\"" |>> f .>> spaces) s

let char_quoted s = 
    let char_quoted_body (s: Tokenizer) =
        let inline read on_succ =
            let x = peek s
            if x <> eol then inc s; on_succ x
            else error_char s.from (Expected "character or '")
        read (function
            | '\\' -> 
                read (Ok << function
                    | 'n' -> '\n'
                    | 'r' -> '\r'
                    | 't' -> '\t'
                    | x -> x
                    )
            | x -> Ok x
            )
    let from = s.from
    let f _ x _ = {from=from; nearTo=s.from}, TokValue(LitChar x)
    (pipe3 (skip_char '\'') char_quoted_body (skip_char '\'') f .>> spaces) s

let string_quoted s = 
    let string_quoted_body (s: Tokenizer) =
        let inline read on_succ =
            let x = peek s
            if x <> eol then inc s; on_succ x
            else error_char s.from (Expected "character or \"")
        let rec loop (b : StringBuilder) =
            read (function
                | '\\' -> 
                    read (function
                        | 'n' -> '\n'
                        | 'r' -> '\r'
                        | 't' -> '\t'
                        | x -> x
                        >> b.Append >> loop
                        )
                | '"' -> Ok (b.ToString())
                | x -> b.Append x |> loop
                )
        loop (StringBuilder())
    let from = s.from
    let f _ x = {from=from; nearTo=s.from}, TokValue(LitString x)
    (pipe2 (skip_char '"') string_quoted_body f .>> spaces) s

let brackets s =
    let from = s.from
    let f spec = inc s; (spaces >>% ({from=from; nearTo=s.from}, TokBracket(spec))) s
    match peek s with
    | '(' -> f (Round,Open) | '[' -> f (Square,Open) | '{' -> f (Curly,Open)
    | ')' -> f (Round,Close) | ']' -> f (Square,Close) | '}' -> f (Curly,Close)
    | _ -> error_char s.from (Expected "`(`,`[`,`{`,`}`,`]` or `)`")

let token s =
    let i = s.from
    let inline (+) a b = alt i a b
    (var + symbol + number + string_raw + char_quoted + string_quoted + brackets + comment + operator) s

/// An array of {line: int; char: int; length: int; tokenType: int; tokenModifiers: int} in the order as written suitable for serialization.
type VSCTokenArray = int []
open Config
let process_error (line, ers : (Range * TokenizerError) list) : (string * VSCRange) [] =
    List.toArray ers
    |> Array.groupBy (fun (a,_) -> a.from)
    |> Array.choose (fun (from,ar) ->
        if 0 < ar.Length then
            let near_to, (expecteds, messages) = 
                ar |> Array.fold (fun (near_to, (expecteds, messages)) (a,b) -> 
                    max near_to a.nearTo,
                    match b with
                    | Expected x -> x :: expecteds, messages
                    | Message x -> expecteds, x :: messages
                    ) (Int32.MinValue,([],[]))
            let ex () = match expecteds with [x] -> sprintf "Expected: %s" x | x -> sprintf "Expected one of: %s" (String.concat ", " x)
            let f l = String.concat "\n" l
            if List.isEmpty expecteds then f messages
            elif List.isEmpty messages then ex ()
            else f (ex () :: "" :: "Other error messages:" :: messages)
            |> fun x -> Some(x,({line=line; character=from}, {line=line; character=near_to}))
        else None
        )

type LineToken = Range * SpiralToken
type LineTokenErrors = (Range * TokenizerError) list
type LineTokenResult = LineToken [] * LineTokenErrors
let tokenize text : LineTokenResult =
    let s = {from=0; text=text}
    LineParsers.spaces' s

    let ar = ResizeArray()
    let rec loop () =
        let i = index s
        match token s with
        | Ok _ when i = index s -> failwith "The parser succeeded without changing the parser index in `tokenize`. Had an exception not been raised the parser would have diverged."
        | Ok x -> ar.Add x; loop()
        | Error er -> er
    let ers =
        let ers = loop ()
        let c = peek s
        if c = eol then []
        elif c = '\t' then [range_char (index s), Message "Tabs are not allowed."]
        else ers
    ar.ToArray(), ers

type VSCodeTokenData = int [] * (string * VSCRange) [] // FSharp.Json does not support serializing ResizeArray objects
let vscode_tokens line_delta (lines : LineToken [] []) =
    let toks = ResizeArray()
    lines |> Array.fold (fun line_delta tok ->
        tok |> Array.fold (fun (line_delta,from_prev) (r,x) ->
            toks.AddRange [|line_delta; r.from-from_prev; r.nearTo-r.from; token_groups x; 0|]
            0, r.from
            ) (line_delta, 0)
        |> fst |> ((+) 1)
        ) line_delta
    |> ignore
    
    toks.ToArray()

open Hopac
open Hopac.Infixes
open Hopac.Extensions
open System.Collections.Generic

type SpiEdit = {|from: int; nearTo: int; lines: string []|}

type FileOpenRes = VSCError []
type FileChangeRes = VSCError []
type FileTokenAllRes = VSCTokenArray
type VSCTokenArrayChange = int * int * VSCTokenArray
type FileTokenChangesRes = VSCTokenArrayChange []

type TokReq =
    | Put of string * IVar<FileOpenRes>
    | Modify of SpiEdit [] * IVar<FileChangeRes>
    | GetAll of IVar<VSCTokenArray>
    | GetChanges of IVar<FileTokenChangesRes>

let server = Job.delay <| fun () ->
    let tokens = ResizeArray([[||]])
    let offsets = ResizeArray([0])
    let mutable errors = [||]

    let replace (edit : SpiEdit) =
        while edit.from < offsets.Count-1 do offsets.RemoveAt(offsets.Count-1)

        let toks, ers = Array.map tokenize edit.lines |> Array.unzip
        tokens.RemoveRange(edit.from,edit.nearTo-edit.from)
        tokens.InsertRange(edit.from,toks)

        errors <- errors |> Array.filter (fun (_,(a,_)) -> (edit.from <= a.line && a.line < edit.nearTo) = false)
        errors <- Array.append errors (ers |> Array.mapi (fun i x -> process_error (edit.from+i, x)) |> Array.concat)

    let offset i =
        while offsets.Count-1 < i do let i = offsets.Count-1 in offsets.Add(offsets.[i] + tokens.[i].Length)
        offsets.[i]

    let line_delta i =
        let rec loop i = if i = 0 || 0 < tokens.[i].Length then i else loop (i-1)
        if i = 0 then 0 else i - loop (i-1)

    let tokens_between_length from near_to =
        let rec loop s i = if i < near_to then loop (s + tokens.[i].Length) (i + 1) else s
        loop 0 from

    let tokens_between_extra from near_to =
        let ar = ResizeArray()
        let rec loop_extra i = // On every change, the first token after the range needs to have its line delta adjusted.
            if i < tokens.Count then 
                let x = tokens.[i]
                if 0 < x.Length then ar.Add([|x.[0]|]); 1 else loop_extra (i+1)
            else 0
        let rec loop_between i = if i < near_to then ar.Add(tokens.[i]); loop_between (i + 1)
        loop_between from
        loop_extra near_to, ar.ToArray()

    let req = Ch()
    let changes = Queue()
    let loop =
        Ch.take req >>= function
            | Put(text,res) -> replace {|from=0; nearTo=tokens.Count; lines=Utils.lines text|}; IVar.fill res errors
            | Modify(edits,res) ->
                edits |> Array.map (fun edit ->
                    let offset = offset edit.from
                    let length = tokens_between_length edit.from edit.nearTo
                    replace edit
                    let extra, data = tokens_between_extra edit.from (edit.from + edit.lines.Length)
                    offset*5, (length+extra)*5, vscode_tokens (line_delta edit.from) data
                    )
                |> changes.Enqueue
                IVar.fill res errors
            | GetAll res -> changes.Clear(); vscode_tokens 0 (tokens.ToArray()) |> IVar.fill res
            | GetChanges res -> let x = changes.ToArray() |> Array.concat in changes.Clear(); IVar.fill res x
    Job.foreverServer loop >>-. req