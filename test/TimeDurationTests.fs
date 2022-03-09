module TimeDurationTests

open System
open Xunit
open FerrisTime.Duration

let shouldBeSome value =
    match value with
    | Some x -> x
    | None -> failwith "cannot be none"

let shouldBeOkay value = 
    match value with
    | Ok r -> r
    | Error _ -> failwith $"cannot be error"

let shouldBeError value = 
    match value with
    | Ok _ -> failwith "cannot be ok"
    | Error e -> e

let doesResultEqual result d h m =
    Assert.Equal(d, result.Days)
    Assert.Equal(h, result.Hours)
    Assert.Equal(m, result.Minutes)


[<Fact>]
let ``can parse single minute`` () =
    let value = "8m"
    
    let result = parse value |> shouldBeOkay

    doesResultEqual result 0 0 8

[<Fact>]
let ``can parse single minute with leading whitespace`` () =
    let value = "   8m"
    
    let result = parse value |> shouldBeOkay

    doesResultEqual result 0 0 8

[<Fact>]
let ``can parse single minute with trailing whitespace`` () =
    let value = "8m   "
    
    let result = parse value |> shouldBeOkay

    doesResultEqual result 0 0 8

[<Fact>]
let ``can parse single hour`` () =
    let value = "8h"
    
    let result = parse value |> shouldBeOkay

    doesResultEqual result 0 8 0

[<Fact>]
let ``can parse single hour with leading whitespace`` () =
    let value = "   8h"
    
    let result = parse value |> shouldBeOkay

    doesResultEqual result 0 8 0

[<Fact>]
let ``can parse single day`` () =
    let value = "8d"
    
    let result = parse value |> shouldBeOkay

    doesResultEqual result 8 0 0

[<Fact>]
let ``can parse single day with leading white space`` () =
    let value = "   8d"
    
    let result = parse value |> shouldBeOkay

    doesResultEqual result 8 0 0

[<Fact>]
let ``can return error message when nothing to parse`` () =
    let value = "8"

    let result = parse value |> shouldBeError

    //Assert.Equal("Error in Ln: 1 Col: 2\r\n8\r\n ^\r\nNote: The error occurred at the end of the input stream.\r\nExpecting: 'H' or 'h'\r\n", result)
    Assert.Equal("'8' cannot be parsed", result)

[<Fact>]
let ``can parse hour and minute`` () =
    let value = "8h 15m"

    let result = parse value |> shouldBeOkay
    
    doesResultEqual result 0 8 15

[<Fact>]
let ``can parse day, hour and minute`` () =
    let value = "2d 20h 33m"

    let result = parse value |> shouldBeOkay

    doesResultEqual result 2 20 33