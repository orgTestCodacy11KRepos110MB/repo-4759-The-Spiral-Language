﻿module File1

open Spiral.Lib
open Spiral.Tests
open System.IO
open Learning
open System.Diagnostics

let cfg: Spiral.Types.CompilerSettings = {
    path_cuda90 = @"C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v9.0"
    path_cub = @"C:\cub-1.7.4"
    path_vs2017 = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community"
    cuda_includes = []
    }

rewrite_test_cache cfg None //(Some(0,40))

let example = 
    "example",[option;tuple;loops;extern_;console;host_tensor;cuda],"Module description.",
    """
inl fact to = Loops.for {from=2; to state=dyn 1; body=inl {state i} -> state * i}
openb Cuda
run {
    blockDim=1
    gridDim=1
    kernel=cuda fact 3 |> ignore
    }
    """

//output_test_to_temp {cfg with cuda_includes=["cub/cub.cuh"]} @"C:\Users\Marko\Source\Repos\The Spiral Language\Temporary\output.fs" learning
//output_test_to_temp cfg @"C:\Users\Marko\Source\Repos\The Spiral Language\Temporary\output.fs" example
//|> printfn "%s"
//|> ignore

