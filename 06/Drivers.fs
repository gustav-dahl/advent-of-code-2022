module AdventOfCode.Drivers

open AdventOfCode.Utilities

// ----------------------------------------------------------------
// Part 1

let expected1 = 7

let part1 (input: string[]) =
    let count = 4

    let result =
        input
        |> Array.head
        |> Seq.toList
        |> Seq.windowed count
        |> Seq.map Set.ofSeq
        |> Seq.findIndex (fun x -> x.Count = count)

    result + count

// ----------------------------------------------------------------
// Part 2

let expected2 = 19

let part2 input =
    let count = 14

    let result =
        input
        |> Array.head
        |> Seq.toList
        |> Seq.windowed count
        |> Seq.map Set.ofSeq
        |> Seq.findIndex (fun x -> x.Count = count)

    result + count
