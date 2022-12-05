open System.Text.RegularExpressions
open System.Linq

let x = "move 1 from 2 to 1"
let result = Regex.Match(@"move (\d+) from (\d+) to (\d+)", x)

let foo = result.Groups.Values.ToArray()
