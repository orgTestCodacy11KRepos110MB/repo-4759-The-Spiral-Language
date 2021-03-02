﻿module Spiral.Blockize

//open FSharpx.Collections
//open VSCTypes
//open Spiral.Tokenize
//open Spiral.BlockSplitting
//open Spiral.BlockParsing

//type FileOpenRes = Block list * RString []
//type FileChangeRes = Block list * RString []
//type FileTokenAllRes = VSCTokenArray

//open Spiral.TypecheckingUtils
//type ParsedBlock = {parsed: Result<TopStatement, (VSCRange * ParserErrors) list> * LineTokens; offset: int}
//let block_bundle (l : (_ * ParsedBlock) list) =
//    let (+.) a b = Tokenize.add_line_to_range a b
//    let bundle = ResizeArray()
//    let errors = ResizeArray<RString>()
//    let temp = ResizeArray()
//    let move_temp () = if 0 < temp.Count then bundle.Add(Seq.toList temp); temp.Clear()
//    let rec init (l : (_ * ParsedBlock) list) =
//        match l with
//        | (_,x) :: x' ->
//            match fst x.parsed with
//            | Ok (TopAnd(r,_)) -> errors.Add(x.offset +. r, "Invalid `and` statement."); init x'
//            | Ok (TopRecInl _ as a) -> temp.Add {offset=x.offset; statement=a}; recinl x'
//            | Ok (TopNominalRec _ as a) -> temp.Add {offset=x.offset; statement=a}; rectype x'
//            | Ok a -> temp.Add {offset=x.offset; statement=a}; move_temp(); init x'
//            | Error er -> BlockParsingError.show_block_parsing_error x.offset er |> errors.AddRange; init x'
//        | [] -> move_temp()
//    and recinl (l : (_ * ParsedBlock) list) =
//        match l with
//        | (_,x) :: x' ->
//            match fst x.parsed with
//            | Ok (TopAnd(_, TopRecInl _ & a)) -> temp.Add {offset=x.offset; statement=a}; recinl x'
//            | Ok (TopAnd(r, _)) -> errors.Add(x.offset +. r, "inl/let recursive statements can only be followed by `and` inl/let statements."); recinl x'
//            | Ok _ -> move_temp(); init l
//            | Error er -> BlockParsingError.show_block_parsing_error x.offset er |> errors.AddRange; recinl x'
//        | [] -> move_temp()
//    and rectype (l : (_ * ParsedBlock) list) =
//        match l with
//        | (_,x) :: x' ->
//            match fst x.parsed with
//            | Ok (TopAnd(_, TopNominalRec _ & a)) -> temp.Add {offset=x.offset; statement=a}; rectype x'
//            | Ok (TopAnd(r, _)) -> errors.Add(x.offset +. r, "`union rec` can only be followed by `and union`."); rectype x'
//            | Ok _ -> move_temp(); init l
//            | Error er -> BlockParsingError.show_block_parsing_error x.offset er |> errors.AddRange; rectype x'
//        | [] -> move_temp()
//    init l

//    let line_tokens = List.fold (fun s (_,x) -> PersistentVector.append s (snd x.parsed)) PersistentVector.empty l
//    line_tokens, Seq.toList bundle, Seq.toList errors

//let semantic_updates_apply (block : LineTokens) updates =
//    Seq.fold (fun block (c : VectorCord,l) -> 
//        let x =
//            let r, x = PersistentVector.nthNth c.row c.col block
//            let x =
//                match x with
//                | TokVar(a,_) -> TokVar(a,l)
//                | TokSymbol(a,_) -> TokSymbol(a,l)
//                | TokSymbolPaired(a,_) -> TokSymbolPaired(a,l)
//                | TokOperator(a,_) -> TokOperator(a,l)
//                | TokUnaryOperator(a,_) -> TokUnaryOperator(a,l)
//                | x -> failwithf "Compiler error: Cannot change the semantic legend for the %A token." x
//            r, x
//        PersistentVector.updateNth c.row c.col x block
//        ) block updates

//let block_init is_top_down (block : LineTokens) =
//    let comments, cords_tokens = 
//        Array.init block.Length (fun line ->
//            let x = block.[line]
//            let comment, len = match PersistentVector.tryLast x with Some (r, TokComment c) -> Some (r, c), x.Length-1 | _ -> None, x.Length
//            let tokens = Array.init len (fun i ->
//                let r, x = x.[i] 
//                {|row=line; col=i|}, (({| line=line; character=r.from |}, {| line=line; character=r.nearTo |}), x)
//                )
//            comment, tokens
//            )
//        |> Array.unzip
//    let cords, tokens = Array.unzip (Array.concat cords_tokens)

//    let semantic_updates = ResizeArray()
//    let env : BlockParsing.Env = {
//        tokens_cords = cords; semantic_updates = semantic_updates
//        comments = comments; tokens = tokens; i = ref 0; is_top_down = is_top_down
//        }
//    BlockParsing.parse env, semantic_updates_apply block semantic_updates