// Thanks to @jovaneyck for helping me in finding the solution to Day 7
// https://github.com/jovaneyck/advent-of-code-2022
open System.IO

let loadTestInput =
    let filename = Path.Combine(__SOURCE_DIRECTORY__, $"data/example.txt")

    File.ReadAllLines(filename)

let loadInput =
    let filename = Path.Combine(__SOURCE_DIRECTORY__, $"data/input.txt")

    File.ReadAllLines(filename)

let split (separators: string) (x: string) = x.Split(separators) |> List.ofArray

let parseInput (input: string[]) = input |> List.ofArray

type Command = { Name: string; Args: string }

type Dir = { Name: string }

type File = { Name: string; Size: int }

type Output =
    | Dir of Dir
    | File of File

type Line =
    | Command of Command
    | Output of Output

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


let dict = new System.Collections.Generic.Dictionary<string list, int64>()

let processLine (state: State) line =
    let result =
        match line with

        | Command c when c.Name = "cd" && c.Args = ".." -> { state with CurrentPath = state.CurrentPath |> List.skip 1 }

        | Command c when c.Name = "cd" -> { state with CurrentPath = c.Args :: state.CurrentPath }

        | Output o -> { state with Tree = insert state.Tree state.CurrentPath o }

        | _ -> state

    result

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

let input = loadInput |> parseInput
let lines = input |> List.map (fun x -> parseLine x)
let state = lines |> List.fold processLine initialState
let paths = state.Tree |> Map.keys |> Seq.toList

dict.Clear()

let sizes = paths |> List.map (fun dir -> dir, dir |> calculateSize state.Tree)

let sumOfLargeFolders =
    sizes |> List.filter (fun (_, s) -> s <= 100000L) |> Seq.sumBy snd

printfn "%A" sumOfLargeFolders

let totalDiskSpace = 70000000L
let unusedDiskSpace = totalDiskSpace - (sizes |> List.find (fun (p, _) -> p = [ "/" ]) |> snd)
let diskSpaceToFree = 30000000L - unusedDiskSpace

let result2 =
    sizes |> List.sortBy snd |> List.find (fun (_, size) -> size >= diskSpaceToFree)

printfn "%A" result2
