namespace FerrisTime

module Say =
    let hello name =
        printfn "Hello %s" name

module TimeDuration =
    open FParsec
    type Interval = { Hours: int }

    type Unit = 
    | Hour of int
    

    let str s = pstring s
    let phour = pint32 .>> (pchar 'h' <|> pchar 'H') |>> fun x -> Hour(x)

    
    let parse (value: string): Result<Interval, string> =
        let result = run phour value
        match result with 
        | Success (u, _, _) -> 
            match u with 
            | Hour x -> Microsoft.FSharp.Core.Result<_,_>.Ok { Hours = x }
        | Failure (m, _, _) -> Microsoft.FSharp.Core.Result<_,_>.Error m