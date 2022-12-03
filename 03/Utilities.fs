
module AdventOfCode.Utilities

let split (separators: string) (x: string) = x.Split(separators)

let flattenList x = x |> List.reduce List.append
let flattenArray x = x |> Array.reduce Array.append