module AdventOfCode.Utilities

let split (separators: string) (x: string) = x.Split(separators) |> List.ofArray

let parseInput (input: string[]) = input |> List.ofArray
