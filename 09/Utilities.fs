module AdventOfCode.Utilities

let split (separators: string) (x: string) = x.Split(separators) |> List.ofArray

let clamp (x: int) (a, b) = System.Math.Clamp(x, a, b)

let distinct x = x |> Set.ofList

let last x = x |> List.map List.last

let replace index sub =
    List.mapi (fun i x -> if i = index then sub else x)

let flatten x = x |> List.reduce List.append
