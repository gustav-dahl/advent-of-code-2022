module AdventOfCode.Input

open System.IO

let loadTestInput =
    let filename = Path.Combine(__SOURCE_DIRECTORY__, $"data/example.txt")

    File.ReadAllLines(filename)

let loadInput =
    let filename = Path.Combine(__SOURCE_DIRECTORY__, $"data/input.txt")

    File.ReadAllLines(filename)
