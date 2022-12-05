module AdventOfCode.Drivers

open System
open AdventOfCode.Utilities

let isNotEmpty x = String.IsNullOrEmpty x = false

let stringFromCharList (cl: char list) = String.concat "" <| List.map string cl

let remove i x = x |> List.take (x.Length - i)

let parseInstruction x =
    let result = x |> split " "

    (int result[1], int result[3] - 1, int result[5] - 1)

let removeEmptyValues x = x |> List.filter (fun y -> y <> ' ')

let applyInstruction9000 (x: list<list<char>>) (amount, src, dst) =

    let a = x[src] |> List.rev |> List.take amount
    let b = x[dst] @ a
    let c = x[src] |> remove amount
    let d = x |> List.updateAt dst b |> List.updateAt src c

    d

let applyInstruction9001 (x: list<list<char>>) (a, s, d) =

    let A = x[s] |> List.rev |> List.take a |> List.rev
    let B = x[d] @ A

    let C = List.updateAt d B x
    let D = x[s] |> remove a
    let E = List.updateAt s D C

    E

let parseState x =
    x
    |> replace "    " "[-]"
    |> replace " " ""
    |> replace "-" " "
    |> replace "[" ""
    |> replace "]" ""

let parseInput (input: string[]) =
    let emptyRowIndex = input |> Array.findIndex (fun x -> String.IsNullOrEmpty x)
    let parts = input |> Array.splitAt emptyRowIndex

    let state =
        fst parts
        |> Array.toList
        |> remove 1
        |> List.map parseState
        |> List.map List.ofSeq
        |> List.transpose
        |> List.map removeEmptyValues
        |> List.map List.rev

    //printList state

    let instructions =
        snd parts
        |> Array.filter isNotEmpty
        |> Array.map parseInstruction
        |> Array.toList

    //printList instructions

    (state, instructions)

// ----------------------------------------------------------------
// Part 1

let expected1 = "CMZ"

let part1 (input: string[]) =
    let state, instructions = input |> parseInput

    let foo =
        instructions
        |> List.fold applyInstruction9000 state
        |> List.map List.rev
        |> List.map List.head
        |> stringFromCharList

    foo

// ----------------------------------------------------------------
// Part 2

let expected2 = "MCD"

let part2 input =
    let state, instructions = input |> parseInput

    let foo =
        instructions
        |> List.fold applyInstruction9001 state
        |> List.map List.rev
        |> List.map List.head
        |> stringFromCharList

    foo
