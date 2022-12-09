open System.IO

//let loadInput =
//    let filename = Path.Combine(__SOURCE_DIRECTORY__, $"data/example.txt")

//    File.ReadAllLines(filename)

let loadInput =
    let filename = Path.Combine(__SOURCE_DIRECTORY__, $"data/input.txt")

    File.ReadAllLines(filename)


// Utilities
let split (separators: string) (x: string) = x.Split(separators) |> List.ofArray

let flatten x = x |> List.reduce List.append

type Move = string
type Position = int * int

// Part 1

let parseMove (line: list<string>) : list<Move> =
    let [ direction; steps ] = line
    let count = int steps
    let moves = List.replicate count direction
    moves


let parseInput (lines: string[]) =
    lines |> List.ofArray |> List.map (split " ")

let calculateHeadPosition (x, y) move =

    let position =
        match move with
        | "L" -> (x - 1, y)
        | "R" -> (x + 1, y)
        | "U" -> (x, y + 1)
        | "D" -> (x, y - 1)

    (Position position)

let calculateTailPosition newHead oldHead tail =
    let (x1, y1) = newHead
    let (x2, y2) = tail

    let distance = List.max [ abs (x2 - x1); abs (y2 - y1) ]

    if distance > 1 then oldHead else tail

let initialState = [ ((0, 0), (0, 0)) ]

let processMoves (moves: list<Move>) =

    let processMove (positions: list<Position * Position>) (move: Move) =
        let (head, tail) = positions |> List.last

        let newHead = calculateHeadPosition head move
        let newTail = calculateTailPosition newHead head tail

        positions @ [ (newHead, newTail) ]

    let result = List.fold processMove initialState moves
    result

let input = loadInput |> parseInput |> List.map parseMove
let foo = input |> flatten |> processMoves |> List.map (fun (_, tail) -> tail) |> Set.ofList
foo.Count

//printfn "%A" asd
