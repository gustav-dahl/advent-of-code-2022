
module AdventOfCode.Utilities

let split (separators: string) (x: string) = x.Split(separators)

let flattenList x = x |> List.reduce List.append
let flattenArray x = x |> Array.reduce Array.append

let intersect s1 s2 = Set.intersect (set s1) (set s2) |> Set.toArray