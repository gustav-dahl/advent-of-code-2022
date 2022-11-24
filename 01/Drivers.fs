module AdventOfCode.Drivers

let parseInput (inputLines: string[]) = inputLines |> Array.map int

let isIncrease (n1, n2) = n1 < n2

let part1 (input: string[]) =
    input |> Array.pairwise |> Array.filter isIncrease |> Array.length

let part2 input = 2000
