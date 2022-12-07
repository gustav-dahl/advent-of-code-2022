module AdventOfCode.Types

type Command = { Name: string; Args: string }

type Dir = { Name: string }

type File = { Name: string; Size: int }

type Output =
    | Dir of Dir
    | File of File

type Line =
    | Command of Command
    | Output of Output
