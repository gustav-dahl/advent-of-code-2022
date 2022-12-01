module AdventOfCode.Drivers

let split (separators: string) (x: string) = x.Split(separators)

let parseInput (input: string[]) =
    input
    |> String.concat "|"
    |> split "||"
    |> Seq.map (split "|")
    |> Seq.map (fun x -> x |> Seq.map int)

let part1 (input: string[]) =
    let elfs = input |> parseInput
    let calories = elfs |> Seq.map Seq.sum
    let maxCalories = calories |> Seq.max

    maxCalories

let part2 input =
    let elfs = input |> parseInput
    let calories = elfs |> Seq.map Seq.sum

    let maxCalories =
        calories
        |> Seq.sortDescending
        |> Seq.take 3
        |> Seq.sum

    maxCalories
