module AdventOfCode.Drivers

open AdventOfCode.Utilities

let parse (input: string[]) =
    input |> Array.map (split " ") |> List.ofArray

let asInstructions (lines: list<list<string>>) =

    let parse line =
        match line with
        | [ "noop" ] -> [ 0 ]
        | [ "addx"; x ] -> [ 0; int x ]

    lines |> List.map parse |> flatten

let updateState state v =
    let x = state |> List.last
    state @ [ x + v ]

let executeWith initialState list = 
        list
        |> List.fold updateState initialState
        |> List.mapi (fun i x -> (i, x))

let signalStrength (cycles: list<int * int>) i =
    let (_, x) = cycles[i]
    x * i

let render (buffer: char[]) (pixel, x) =
    let sprite = [ (x - 1) .. (x + 1) ]
    let character = sprite |> List.contains (pixel % 40) |> mapTo '#' ' '

    buffer |> replace pixel character

let display (buffer: char[]) =
    printfn ""

    let matrix = buffer |> Array.chunkBySize 40

    for i in [ 0..5 ] do
        let row = matrix[i] |> join
        printfn "%A" row

    printfn ""

let initialState = [ 1 ]

// ----------------------------------------------------------------
// Part 1

let expected1 = 13027

let part1 (input: string[]) =

    let instructions = parse input |> asInstructions

    let cycles = instructions |> executeWith initialState

    let checkpoints = [ 20 ] @ [ 60..40 .. cycles.Length ] |> List.map (minus 1)

    let result = checkpoints |> List.map (signalStrength cycles) |> List.sum

    result

// ----------------------------------------------------------------
// Part 2

let screen = Array.init (40 * 6) (fun _ -> ' ')

let expected2 = 0

let part2 input =

    let instructions = parse input |> asInstructions

    let cycles = instructions |> executeWith initialState

    let result = cycles |> Seq.fold render screen

    display result
    0
