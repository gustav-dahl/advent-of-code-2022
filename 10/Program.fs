// https://mallibone.com/post/advent-of-f-code

open System
open AdventOfCode.Input
open AdventOfCode.Drivers
open AdventOfCode.Outout

Console.Clear()

Console.ForegroundColor <- ConsoleColor.Gray
printfn "------- Part 1 -------"

// --------------------------------------------
// Test - Part 1
let testValue1 = loadTestInput |> part1
let isSuccess1 = isSuccess testValue1 expected1
print "Test: %A" testValue1 isSuccess1

// --------------------------------------------
// Part 1
let result1 = loadInput |> part1
print "Result: %A" result1 isSuccess1 

Console.ForegroundColor <- ConsoleColor.Gray
printfn "------- Part 2 -------"

// --------------------------------------------
// Test -Part 2
let testValue2 = loadTestInput |> part2
let isSuccess2 = isSuccess testValue2 expected2
print "Test: %A" testValue2 isSuccess2

// --------------------------------------------
// Part 2
let result2 = loadInput |> part2
print "Result: %A" result2 isSuccess2
