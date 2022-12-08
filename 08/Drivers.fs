
module AdventOfCode.Drivers

open AdventOfCode.Utilities

let parseInput (input: string[]) =
    input
    |> Array.map Array.ofSeq
    |> Array.map (fun x -> x |> Array.map (string >> int))
    |> toArray2D

let find func (x, y) (matrix: int[,]) =

    let row = matrix[x, *]
    let left = row[..y] |> func
    let right = row[y..] |> Array.rev |> func

    let column = matrix[*, y]
    let up = column[..x] |> func
    let down = column[x..] |> Array.rev |> func

    (left, right, up, down)


// ----------------------------------------------------------------
// Part 1

let visible (x: int[]) =
    let y = x |> Array.rev
    let head = y |> Array.head
    let tail = y |> Array.tail

    let isVisible = head > (tail |> Array.max)
    isVisible

let expected1 = 21

let part1 (input: string[]) =

    let matrix = input |> parseInput
    let range = Array2D.create (matrix.GetLength(0) - 2) (matrix.GetLength(1) - 2) 0

    let visibleTrees =
        range
        |> Array2D.mapi (fun x y _ -> find visible (x + 1, y + 1) matrix)
        |> flatten
        |> Seq.map (fun (left, right, up, down) -> left || right || up || down)

    let numberOfEdgeTrees = matrix.GetLength(0) * 4 - 4
    let numberOfVisibleTrees =
        (visibleTrees |> Seq.filter (fun x -> x = true) |> Seq.length) 

    let result = numberOfEdgeTrees + numberOfVisibleTrees
    result

// ----------------------------------------------------------------
// Part 2

let viewingDistance (x: int[]) =
    let y = x |> Array.rev
    let head = y |> Array.head
    let tail = y |> Array.tail

    let distance =
        if tail |> Array.max < head then
            tail.Length
        else
            (tail |> Array.takeWhile (fun i -> i < head) |> Array.length) + 1
    distance

let expected2 = 8

let part2 input =
    
    let matrix = input |> parseInput
    let range = Array2D.create (matrix.GetLength(0) - 2) (matrix.GetLength(1) - 2) 0

    let viewingDistances =
        range
        |> Array2D.mapi (fun x y _ -> find viewingDistance (x + 1, y + 1) matrix)
        |> flatten
        |> Seq.map (fun (left, right, up, down) -> left * right * up * down)

    let result = viewingDistances |> Seq.max
    result


