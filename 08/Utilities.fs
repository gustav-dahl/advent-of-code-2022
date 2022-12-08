module AdventOfCode.Utilities

let split (separators: string) (x: string) = x.Split(separators) |> List.ofArray

let toArray2D (x: int[][]) =
    Array2D.init x.Length x.Length (fun i j -> x[i][j])

let flatten array2D =
    seq {
        for x in [ 0 .. (Array2D.length1 array2D) - 1 ] do
            for y in [ 0 .. (Array2D.length2 array2D) - 1 ] do
                yield array2D.[x, y]
    }
