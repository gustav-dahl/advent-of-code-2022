module AdventOfCode.Drivers

open AdventOfCode.Utilities

let parseRows line =
    line
    |> split ","
    |> Array.map (fun x -> split "-" x)
    |> Array.map (fun x -> (int x[0], int x[1]))
    |> Array.map (fun (x, y) -> [ x..y ])
    |> Array.map (fun x -> set x)
    |> (fun x -> (x[0], x[1]))

// ----------------------------------------------------------------
// Part 1

let isFullOverlap (s1, s2) =
    Set.isSubset s1 s2 || Set.isSubset s2 s1

let expected1 = 2

let part1 (input: string[]) =
    let rows = input |> Array.map parseRows

    let overlaps = rows |> Array.map isFullOverlap |> Array.filter id |> Array.length

    overlaps

// ----------------------------------------------------------------
// Part 2

let isOverlap (s1, s2) = Set.intersect s1 s2

let expected2 = 4

let part2 input =

    let rows = input |> Array.map parseRows

    let overlaps =
        rows
        |> Array.map isOverlap
        |> Array.filter (fun x -> (Set.isEmpty x) = false)
        |> Array.length

    overlaps
