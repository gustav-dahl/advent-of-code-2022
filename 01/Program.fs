// https://mallibone.com/post/advent-of-f-code

open AdventOfCode.Input
open AdventOfCode.Drivers
open AdventOfCode.Outout

let expected1 = 24000 
let expected2 = 45000

// --------------------------------------------
// Test - Part 1
let testValue1 = loadTestInput |> part1
let testResult1 = isSuccess testValue1 expected1
print "Test 1: %A" testValue1 testResult1

// --------------------------------------------
// Part 1
let result1 = loadInput |> part1
print "Result 1: %A" result1 Info

// --------------------------------------------
// Test -Part 2
let testValue2 = loadTestInput |> part2
let testResult2 = isSuccess testValue2 expected2
print "Test 2: %A" testValue2 testResult2

// --------------------------------------------
// Part 2
let result2 = loadInput |> part2
print "Result 2: %A" result2 Info
