module AdventOfCode.Drivers

open AdventOfCode.Utilities

type Direction = string
type Position = int * int

let parse (lines: string[]) =
    lines |> List.ofArray |> List.map (split " ")

let asMovements (lines: list<list<string>>) : list<Direction> =

    let parse (line: list<string>) =
        let direction = line[0]
        let count = int line[1]
        let moves = List.replicate count direction
        moves

    lines |> List.map parse |> flatten

let move direction (x, y) =

    let position =
        match direction with
        | "L" -> (x - 1, y)
        | "R" -> (x + 1, y)
        | "U" -> (x, y + 1)
        | "D" -> (x, y - 1)

    position

let follow (x2, y2) (x1, y1) : Position =

    let dx = x2 - x1
    let dy = y2 - y1

    let position =
        if abs dx <= 1 && abs dy <= 1 then
            (x1, y1)
        else
            (x1 + (clamp dx (-1, 1)), y1 + (clamp dy (-1, 1)))

    position

let simulate (direction: Direction) (rope: list<Position>) i =

    let position =
        if i = 0 then
            rope[0] |> move direction
        else
            rope[i] |> follow rope[i - 1]

    let result = rope |> replace i position

    result

let updateKnots (history: list<list<Position>>) (direction: Direction) =

    let rope = history |> List.last
    let knots = [ 0 .. rope.Length - 1 ]
    let result = knots |> List.fold (simulate direction) rope

    history @ [ result ]

let updateRope count (moves: list<Direction>) =

    let knots: list<Position> = List.replicate count (0, 0)
    let history = [ knots ]
    let result = moves |> List.fold updateKnots history

    result

// ----------------------------------------------------------------
// Part 1

let expected1 = 13

let part1 (input: string[]) =

    let movements = parse input |> asMovements

    let ropeLength = 2
    let history = updateRope ropeLength movements

    let result = history |> last |> distinct |> Set.count
    result

// ----------------------------------------------------------------
// Part 2

let expected2 = 1

let part2 input =

    let movements = parse input |> asMovements

    let ropeLength = 10
    let history = movements |> updateRope ropeLength

    let result = history |> last |> distinct |> Set.count
    result
