module AdventOfCode.Drivers

open AdventOfCode.Utilities
open FSharpPlus
open System

let length x = String.length x
let halfLength x = (length x) / 2

let asSet (s1, s2) = (Set.ofList s1, Set.ofList s2)
let inBoth (s1, s2) = Set.intersect s1 s2


let calculateScore c =
    match c with
    | c when Char.IsLower(c) -> int c - 112 + 16
    | c -> int c - 76 + 38


let parseItems rows =
    rows
    |> Array.map (fun items -> (items, halfLength items))
    |> Array.map (fun (items, l) -> (items[.. (l - 1)], items[l..]))
    |> Array.map (fun (s1, s2) -> (String.toList s1, String.toList s2))
    |> Array.map asSet
    |> Array.map inBoth
    |> Array.map Set.toList
    |> Array.toList

// ----------------------------------------------------------------
// Part 1

let expected1 = 157

let part1 (input: string[]) =
    let score = input |> parseItems |> flattenList |> List.map calculateScore |> List.sum

    score

// ----------------------------------------------------------------
// Part 2

let expected2 = 70

let parseGroup x =
    x |> Array.map Set.ofList |> Set.intersectMany |> Set.toArray


let part2 input =

    let groups = input |> Array.map Seq.toList |> Array.splitInto (input.Length / 3)

    let score =
        groups
        |> Array.map parseGroup
        |> flattenArray
        |> Array.map calculateScore
        |> Array.sum

    score
