module AdventOfCode.Drivers

open AdventOfCode.Utilities

type Play =
    | Rock
    | Paper
    | Scissors


let parsePlay x =
    match x with
    | "A" -> Rock
    | "B" -> Paper
    | "C" -> Scissors
    | "X" -> Rock
    | "Y" -> Paper
    | "Z" -> Scissors

type Result =
    | Win
    | Loss
    | Draw

let parseResult x =
    match x with
    | "X" -> Loss
    | "Y" -> Draw
    | "Z" -> Win


let calculatePlay round =
    match round with
    | (x, Draw) -> (x, x)
    | (Rock, Win) -> (Rock, Paper)
    | (Paper, Win) -> (Paper, Scissors)
    | (Scissors, Win) -> (Scissors, Rock)
    | (Rock, Loss) -> (Rock, Scissors)
    | (Paper, Loss) -> (Paper, Rock)
    | (Scissors, Loss) -> (Scissors, Paper)


let isWin (x, y) =
    match (y, x) with
    // Rock defeats Scissors
    | (Rock, Scissors) -> Win
    // Paper defeats Rock.
    | (Paper, Rock) -> Win
    // Scissors defeats Paper
    | (Scissors, Paper) -> Win
    // If both players choose the same shape, the round instead ends in a draw.
    | (x, y) -> if x = y then Draw else Loss

let playScore (_, x) =
    match x with
    | Rock -> 1
    | Paper -> 2
    | Scissors -> 3

let outcomeScore x =
    match x with
    | Win -> 6
    | Draw -> 3
    | Loss -> 0

let singleRoundScore round =
    (isWin round |> outcomeScore) + (playScore round)


// ----------------------------------------------------------------
// Part 1

let parseInput1 (input: string[]) =
    input
    |> Array.map (split " ")
    |> Array.map (fun row -> (parsePlay row[0], parsePlay row[1]))

let expected1 = 15

let part1 (input: string[]) =
    let score = input |> parseInput1 |> Array.map singleRoundScore |> Seq.sum

    score

// ----------------------------------------------------------------
// Part 2

let parseInput2 (input: string[]) =
    input
    |> Array.map (split " ")
    |> Array.map (fun row -> (parsePlay row[0], parseResult row[1]))

let expected2 = 12

let part2 input =

    let score =
        input
        |> parseInput2
        |> Array.map calculatePlay
        |> Array.map singleRoundScore
        |> Seq.sum

    score 
