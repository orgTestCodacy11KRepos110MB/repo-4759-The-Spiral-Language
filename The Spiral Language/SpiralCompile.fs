﻿module Spiral.Compile

open Spiral.Types
open Spiral.Show
open Spiral.Codegen
open System.Collections.Generic
open System.Diagnostics
open System

type Timings =
    {
    parse : TimeSpan
    prepass : TimeSpan
    peval : TimeSpan
    codegen : TimeSpan
    }

    member x.Add(y) =
        {
        parse = x.parse + y.parse
        prepass = x.prepass + y.prepass
        peval = x.peval + y.peval
        codegen = x.codegen + y.codegen
        }

type Watches = 
    {
    parse : Stopwatch
    prepass : Stopwatch
    peval : Stopwatch
    codegen : Stopwatch
    }

    member x.Elapsed: Timings =
        {
        parse = x.parse.Elapsed
        prepass = x.prepass.Elapsed
        peval = x.peval.Elapsed
        codegen = x.codegen.Elapsed
        }

let inline timeit (d: Stopwatch) f x =
    d.Start()
    let x = f x
    d.Stop()
    x

type ModulePrepassEnv = {
    settings : SpiralCompilerSettings
    seq : ResizeArray<TypedBind>
    context : ResizeArray<TypedData>
    map : Map<string, int>
    timing : Watches
    }

let raise_compile_error x = raise (CompileError x)
let module_let (env: ModulePrepassEnv) (m: SpiralModule) = 
    let count = env.context.Count
    let context = env.context.ToArray()
    let expr, size = 
        match timeit env.timing.parse (Parsing.parse env.settings) m with
        | Ok x -> x
        | Fail x -> raise_compile_error x
        //|> fun x -> printfn "%A" x; x
        |> timeit env.timing.prepass (Prepass.prepass {prepass_context=context; prepass_map=env.map; prepass_map_length=count})
    let module_ = 
        let d = {rbeh=AnnotationDive; seq=env.seq; env_global=context; env_stack_ptr=0; env_stack=Array.zeroCreate size; trace=[]; cse=ref Map.empty}
        //printfn "%A" expr
        timeit env.timing.peval (PartEval.partial_eval d) expr
    env.context.Add module_
    {env with map=env.map.Add (m.name, count)}

let module_open (env: ModulePrepassEnv) x =
    match env.map.TryFind x with
    | Some x ->
        match env.context.[x] with
        | TyMap x ->
            let map, _ =
                Map.fold (fun (s, count) k v ->
                    env.context.Add v
                    Map.add (keyword_to_string k) count s, count+1
                    ) (env.map, env.context.Count) x
            {env with map=map}
        | x -> raise_compile_error <| sprintf "Expected as module in `module_open`.\nGot: %s" (show_typed_data x)
    | _ -> raise_compile_error <| sprintf "In module_open, `open` did not find a module named %s in the environment." x

let compile (settings: SpiralCompilerSettings) (m: SpiralModule) =
    let env = {
        settings = settings
        context = ResizeArray()
        seq = ResizeArray()
        map = Map.empty
        timing =
            {
            parse = Stopwatch()
            prepass = Stopwatch()
            peval = Stopwatch()
            codegen = Stopwatch()
            }
        }

    try
        let ms =
            let dict = Dictionary(HashIdentity.Reference)
            let ms = ResizeArray()
            let rec loop (m: SpiralModule) =
                memoize dict (fun (m: SpiralModule) ->
                    List.iter loop m.prerequisites
                    ms.Add m
                    ) m
            
            loop m
            ms.ToArray()

        //let env = module_let env CoreLib.core
        //let env = module_open env "Core"
        let env = Array.fold module_let env ms
        env.timing.Elapsed, env.seq.ToArray() 
        //|> fun x -> printfn "%A" x; x
        |> timeit env.timing.codegen Fsharp.codegen
    with
        | :? PrepassError as x -> env.timing.Elapsed, x.Data0
        | :? PrepassErrorWithPos as x -> env.timing.Elapsed, show_trace {settings with filter_list=[]} [x.Data0] x.Data1
        | :? TypeError as x -> env.timing.Elapsed, show_trace settings x.Data0 x.Data1
        | :? TypeRaised as x -> env.timing.Elapsed, sprintf "Uncaught type raise.\nGot: %s" (Show.show_ty x.Data0)
        | :? CodegenError as x -> env.timing.Elapsed, x.Data0
        | :? CodegenErrorWithPos as x -> env.timing.Elapsed, show_trace {settings with filter_list=[]} x.Data0 x.Data1
        | :? CompileError as x -> env.timing.Elapsed, x.Data0

