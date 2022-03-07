namespace FerrisTime

module Say =
    let hello name =
        printfn "Hello %s" name

module TimeDuration =
    open FParsec
    type Interval = { Days: int; Hours: int; Minutes: int }

    type Unit = 
    | Day of int
    | Hour of int
    | Minute of int

    let pday = pint32 .>> (pchar 'd' <|> pchar 'D') |>> fun x -> Day(x)
    let phour = pint32 .>> (pchar 'h' <|> pchar 'H') |>> fun x -> Hour(x)
    let pminute = pint32 .>> (pchar 'm' <|> pchar 'M') |>> fun x -> Minute(x)
    let attemptDay = attempt pday
    let attemptHour = attempt phour
    let attemptMinute = attempt pminute
    let pduration = attemptDay <|> attemptHour <|> attemptMinute
    
    let parse (value: string): Result<Interval, string> =
        let result = run pduration value
        match result with 
        | Success (u, _, _) -> 
            match u with 
            | Day d -> Core.Result<_,_>.Ok { Days = d; Hours = 0; Minutes = 0 }
            | Hour h -> Core.Result<_,_>.Ok { Days = 0; Hours = h; Minutes = 0 }
            | Minute m -> Core.Result<_,_>.Ok { Days = 0; Hours = 0; Minutes = m }
        | Failure (_, _, _) -> Core.Result<_,_>.Error $"'{value}' cannot be parsed"