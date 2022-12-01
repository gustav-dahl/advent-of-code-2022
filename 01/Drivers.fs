module AdventOfCode.Drivers

open FSharpPlus

let parseInput (input: string[]) = input |> map int

let isIncrease (n1, n2) = n1 < n2

let part1 (input: string[]) =
    input |> Seq.pairwise |> filter isIncrease |> length

let part2 input = 2000
