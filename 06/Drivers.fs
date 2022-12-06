module AdventOfCode.Drivers

let processSignal count signals = 
    let result =
        signals
        |> Array.head
        |> Seq.toList
        |> Seq.windowed count
        |> Seq.map Set.ofSeq
        |> Seq.findIndex (fun x -> x.Count = count)

    result + count

// ----------------------------------------------------------------
// Part 1

let expected1 = 7

let part1 (input: string[]) = input |> processSignal 4

// ----------------------------------------------------------------
// Part 2

let expected2 = 19

let part2 input = input |> processSignal 14
