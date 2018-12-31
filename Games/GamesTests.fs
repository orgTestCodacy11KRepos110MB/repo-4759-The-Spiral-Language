﻿module Games.Tests

open Spiral.Lib
open Spiral.Tests
open System.IO
open Module
open Cuda.Lib
open Learning.Lib

let cfg = {Spiral.Types.cfg_default with cuda_assert_enabled=false}

let union1 =
    "union1",[union;option;extern_;console],"Does the to_sparse work?",
    """
inl r = Union.int {from=0; near_to=2}
inl a = Union.to_one_hot (r 0, r (dyn 1), r (dyn 1))
inl r = Union.int {from=0; near_to=10}
inl b = Union.to_one_hot (dyn (Option.none (type r int64)))
inl c = Union.to_one_hot ((Option.some (r 9)))
Console.writeline (a,b,c)
    """

let union2 =
    "union2",[union;option;extern_;console],"Does the from_sparse work?",
    """
inl test x =
    inl a = Union.to_one_hot x
    inl b = Union.from_one_hot x a
    assert (b = x) "The input and output should be equal." 
    Console.writeline b

inl r = Union.int {from=-1; near_to=2}
test (r 0, r (dyn 1), r (dyn 1))
inl r = Union.int {from=-2; near_to=10}
inl rint64 = type r int64

test (dyn (Option.none rint64))
test (Option.some (r 9))

inl Y = (.a,.123) \/ (.b, rint64)
test (box Y (.b, r 3))

inl Action = .Fold \/ .Call \/ (.Raise, rint64)
test (box Action (dyn (.Raise, r 7)))

inl Q = (rint64, rint64, rint64) \/ (rint64, rint64) \/ rint64
test (box Q (dyn (r 3, r 2)))

inl Q = {a=rint64; b=rint64; c=rint64} \/ {a=rint64; b=rint64} \/ {a=rint64}
inl a,b,c = r 3, r 2, r 1
test (join Option.some (box Q {a}))
test (Option.some (box Q {a b}))
test (dyn <| Option.some (box Q {a b c}))
    """

let union3 =
    "union3",[union;option;extern_;console],"Does the to_dense work?",
    """
inl r = Union.int {from=0; near_to=2}
inl a = Union.to_dense (r 0, r (dyn 1), r (dyn 1))
inl r = Union.int {from=0; near_to=10}
inl b = Union.to_dense (dyn (Option.none (type r int64)))
inl c = Union.to_dense (Option.some (r 9))
Console.writeline (a,b,c)
    """

let union4 =
    "union4",[union;option;extern_;console],"Does the from_dense work?",
    """
inl test x =
    inl a = Union.to_dense x
    inl b = Union.from_dense x a
    assert (b = x) "The input and output should be equal." 
    Console.writeline b

inl r = Union.int {from=-1; near_to=2}
test (r 0, r (dyn 1), r (dyn 1))
inl r = Union.int {from=-2; near_to=10}
inl rint64 = type r int64

test (dyn (Option.none rint64))
test (Option.some (r 9))

inl Y = (.a,.123) \/ (.b, rint64)
test (box Y (.b, r 3))

inl Action = .Fold \/ .Call \/ (.Raise, rint64)
test (box Action (dyn (.Raise, r 7)))

inl Q = (rint64, rint64, rint64) \/ (rint64, rint64) \/ rint64
test (box Q (dyn (r 3, r 2)))

inl Q = {a=rint64; b=rint64; c=rint64} \/ {a=rint64; b=rint64} \/ {a=rint64}
inl a,b,c = r 3, r 2, r 1
test (join Option.some (box Q {a}))
test (Option.some (box Q {a b}))
test (dyn <| Option.some (box Q {a b c}))
    """

let grid1 =
    "grid1",[console;loops;union;struct';player_tabular],"The Gridworld (Sea) test.",
    """
inl n = 50
inl reward {row col} =
    if row = col then -0.01 / to float64 (n - 1) + (if row = n - 1 then 1.0 else 0.0)
    else 0.0

inl transition action {row col} ret =
    inl row = row + 1
    if row = n then ret .End
    else
        inl col =
            match action with
            | .Left -> 
                inl col = col - 1
                if col < 0 then 0 else col
            | .Right -> 
                inl col = col + 1
                if col >= n then n - 1 else col           
        ret {row col}

inl Position = Union.int {from=0; near_to=n}
inl observe = Struct.map Position
inl Observation = type observe {row=int64; col=int64}
inl Action = type .Left \/ .Right

inl game player =
    Loops.for' {from=0; near_to=n; state={row=0; col=0}
        body=inl {state i next} ->
            inl action = player.act (observe state)
            transition action state <| function
            | .End -> player.optimize()
            | state -> 
                player.reward (reward state)
                next state
        finally=ignore
        }

inl player = 
    inl step = 10000
    inl step_count = ref 1
    inl {act reward optimize} = PlayerTabular.template {init=1.2f32; elem_type={Observation Action}; learning_rate=0.01f32; trace=0.7f32; discount=1f32}
    //inl act {obs with row col} =
    //    if step_count() = step then 
    //        inl row, col = row.value, col.value
    //        Console.write (row, col)
    //    act obs
    inl reward_sum = ref 0.0
    inl reward rew =
        reward_sum := reward_sum() + rew
        reward rew
    inl optimize x =
        inl k = step_count()
        if k = step then
            Console.printfn "The average of rewards for the past {0} episodes is {1}." (k, (reward_sum() / to float64 k))
            reward_sum := 0.0
            step_count := 0
        step_count := step_count() + 1
        optimize x
    heap {act reward optimize}

inl num_episodes = 150000
Loops.for {from=0; near_to=num_episodes; body=inl {i} -> game player}
    """

output_test_to_temp cfg (Path.Combine(__SOURCE_DIRECTORY__, @"..\Temporary\output.fs")) grid1
|> printfn "%s"
|> ignore

