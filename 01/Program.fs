// https://mallibone.com/post/advent-of-f-code

open AdventOfCode.Input
open AdventOfCode.Drivers
open AdventOfCode.Outout

let expected1 = 7
let expected2 = 1000

// --------------------------------------------
// Part 1
let testResult1 = loadTestInput |> part1

let success1 = if testResult1 = expected1 then Success else Fail
print "Test 1: %A" testResult1 success1

let result1 = loadInput |> part1
print "Result 1: %A" result1 Info

// --------------------------------------------
// Part 2
let testResult2 = loadTestInput |> part2
let success2 = if testResult2 = expected2 then Success else Fail
print "Test 2: %A" testResult2 Fail

let result2 = loadInput |> part2
print "Result 2: %A" result2 Info
