
// Thanks to @jovaneyck for helping me in finding and understanding the solution to Day 7
// https://github.com/jovaneyck/advent-of-code-2022
module AdventOfCode.Drivers

open System.Collections.Generic
open AdventOfCode.Types
open AdventOfCode.Utilities

let parseLine line =
    let parts = line |> split " "

    let result: Line =
        match parts with
        | [ "$"; name; args ] -> Command { Name = name; Args = args }
        | [ "$"; name ] -> Command { Name = name; Args = "" }
        | [ "dir"; name ] -> Output(Dir { Name = name })
        | [ size; name ] -> Output(File { Name = name; Size = int size })
        | _ -> failwith "unknown"

    result

let insert tree path node =
    let atPath = tree |> Map.tryFind path

    let updated =
        match atPath with
        | None -> [ node ]
        | Some things -> node :: things

    tree |> Map.add path updated

type State =
    { CurrentPath: string list
      Tree: Map<string list, Output list> }

let initialState = { CurrentPath = []; Tree = Map.empty }



let processLine (state: State) line =
    let result =
        match line with

        | Command c when c.Name = "cd" && c.Args = ".." -> { state with CurrentPath = state.CurrentPath |> List.skip 1 }

        | Command c when c.Name = "cd" -> { state with CurrentPath = c.Args :: state.CurrentPath }

        | Output o -> { state with Tree = insert state.Tree state.CurrentPath o }

        | _ -> state

    result

let dict = new Dictionary<string list, int>()

let rec calculateSize tree path =
    let exists, size = dict.TryGetValue path

    match exists with
    | true -> size
    | false ->
        let atPath = tree |> Map.find path
        let s = atPath |> List.sumBy (sizeOf tree path)
        dict.TryAdd(path, s) |> ignore
        s

and sizeOf tree path node =
    match node with
    | File x -> x.Size
    | Dir x ->
        let path = x.Name :: path

        let exists, size = dict.TryGetValue path

        match exists with
        | true -> size
        | false ->
            let s = calculateSize tree path
            dict.TryAdd(path, s) |> ignore
            s

// ----------------------------------------------------------------
// Part 1

let expected1 = 95437

let part1 (input: string[]) =

    let lines = input |> parseInput |> List.map (fun x -> parseLine x)
    let state = lines |> List.fold processLine initialState
    let paths = state.Tree |> Map.keys |> Seq.toList

    dict.Clear()

    let sizes = paths |> List.map (fun dir -> dir, dir |> calculateSize state.Tree)

    let result = sizes |> List.filter (fun (_, s) -> s <= 100000) |> Seq.sumBy snd

    result
// ----------------------------------------------------------------
// Part 2

let expected2 = 24933642

let part2 input =

    let lines = input |> parseInput |> List.map (fun x -> parseLine x)
    let state = lines |> List.fold processLine initialState
    let paths = state.Tree |> Map.keys |> Seq.toList

    dict.Clear()

    let sizes = paths |> List.map (fun dir -> dir, dir |> calculateSize state.Tree)

    let totalDiskSpace = 70000000
    let requiredDiskSpace = 30000000

    let unusedDiskSpace =
        totalDiskSpace - (sizes |> List.find (fun (p, _) -> p = [ "/" ]) |> snd)

    let diskSpaceToFree = requiredDiskSpace - unusedDiskSpace

    let result =
        sizes |> List.sortBy snd |> List.find (fun (_, size) -> size >= diskSpaceToFree) |> snd

    result
