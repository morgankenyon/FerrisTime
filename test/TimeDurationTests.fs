module TimeDurationTests

open System
open Xunit
open FerrisTime.TimeDuration

let shouldBeSome value =
    match value with
    | Some x -> x
    | None -> failwith "cannot be none"

let shouldBeOkay value = 
    match value with
    | Ok r -> r
    | Error _ -> failwith "cannot be error"

let shouldBeError value = 
    match value with
    | Ok _ -> failwith "cannot be ok"
    | Error e -> e

[<Fact>]
let ``My test`` () =
    let value = "8h"
    
    let result = parse value |> shouldBeOkay

    Assert.Equal(8, result.Hours)

[<Fact>]
let ``can return none when nothing to parse`` () =
    let value = "8"

    let result = parse value |> shouldBeError

    Assert.Equal("Error in Ln: 1 Col: 2\r\n8\r\n ^\r\nNote: The error occurred at the end of the input stream.\r\nExpecting: 'H' or 'h'\r\n", result)