module TimeIntervalTests

open System
open Xunit
open KlienLib.TimeIntervals

let shouldBeSome value =
    match value with
    | Some x -> x
    | None -> failwith "cannot be none"

[<Fact>]
let ``My test`` () =
    let value = "8h"
    
    let result = parse value |> shouldBeSome

    Assert.Equal(8, result.Hours)
