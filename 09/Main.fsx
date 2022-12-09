open System.IO

//let loadInput =
//    let filename = Path.Combine(__SOURCE_DIRECTORY__, $"data/example.txt")

//    File.ReadAllLines(filename)

let loadInput =
    let filename = Path.Combine(__SOURCE_DIRECTORY__, $"data/input.txt")

    File.ReadAllLines(filename)


// Utilities
let split (separators: string) (x: string) = x.Split(separators) |> List.ofArray

let clamp (x: int) (a, b) = System.Math.Clamp(x, a, b)

let replace index sub =
    List.mapi (fun i x -> if i = index then sub else x)

let flatten x = x |> List.reduce List.append

type Direction = string
type Position = int * int

// Part 1

let parseMovements (line: list<string>) : list<Direction> =
    let [ direction; steps ] = line
    let count = int steps
    let moves = List.replicate count direction
    moves


let parseInput (lines: string[]) =
    lines |> List.ofArray |> List.map (split " ")

let move direction (x, y) =

    let position =
        match direction with
        | "L" -> (x - 1, y)
        | "R" -> (x + 1, y)
        | "U" -> (x, y + 1)
        | "D" -> (x, y - 1)

    (Position position)

let follow (x2, y2) (x1, y1) : Position =

    let dx = x2 - x1
    let dy = y2 - y1

    let position =
        if abs dx <= 1 && abs dy <= 1 then
            (x1, y1)
        else
            (x1 + (clamp dx (-1, 1)), y1 + (clamp dy (-1, 1)))

    position


let processKnot (direction: Direction) (knots: list<Position>) i =

    let position =
        if i = 0 then
            knots[0] |> move direction
        else
            knots[i] |> follow knots[i - 1]

    let result = knots |> replace i position

    result

let processKnots count (history: list<list<Position>>) (move: Direction) =

    let range = [ 0 .. count - 1 ]
    let knots = history |> List.last
    let result = List.fold (processKnot move) knots range

    history @ [ result ]

let processMoves count (moves: list<Direction>) =

    let knots: list<Position> = List.replicate count (0, 0)
    let history = [ knots ]
    let result = List.fold (processKnots count) history moves

    result

let input = loadInput |> parseInput |> List.map parseMovements

let foo =
    input
    |> flatten
    |> processMoves 10
    |> List.map List.last
    |> Set.ofList

foo.Count
