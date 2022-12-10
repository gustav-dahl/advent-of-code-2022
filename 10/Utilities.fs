module AdventOfCode.Utilities

let join (x: char[]) = new string (x)

let mapTo a b x = if x then a else b

let split (separators: string) (x: string) = x.Split(separators) |> List.ofArray

let flatten x = x |> List.reduce List.append

let replace i v (m: char[]) =
    seq {
        for x in [ 0 .. m.GetLength(0) - 1 ] do
            yield if x = i then v else m[x]
    }
    |> Seq.toArray
