module TimeDurationTests

open System
open Xunit
open FerrisTime.TimeDuration

let shouldBeSome value =
    match value with
    | Some x -> x
    | None -> failwith "cannot be none"

[<Fact>]
let ``My test`` () =
    let value = "8h"
    
    let result = parse value |> shouldBeSome

    Assert.Equal(8, result.Hours)
