module AdventOfCode.Utilities

open System

let split (separators: string) (x: string) = x.Split(separators)
let replace (s1: string) (s2: string) (x: string) = x.Replace(s1, s2)

let flattenList x = x |> List.reduce List.append
let flattenArray x = x |> Array.reduce Array.append

let intersect s1 s2 =
    Set.intersect (set s1) (set s2) |> Set.toArray

let max (x: int) (y: int) = if x > y then x else y

let printList lst =
    for x in lst do
        printfn "%A" x
