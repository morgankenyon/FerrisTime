namespace FerrisTime

module Duration =
    open FParsec
    type Interval = { Days: int; Hours: int; Minutes: int }

    type Unit = 
    | Day of int
    | Hour of int
    | Minute of int

    let ws = spaces
    let pday = ws >>. pint32 .>> (pchar 'd' <|> pchar 'D') |>> fun x -> Day(x)
    let phour = ws >>. pint32 .>> (pchar 'h' <|> pchar 'H') |>> fun x -> Hour(x)
    let pminute = ws >>. pint32 .>> (pchar 'm' <|> pchar 'M') |>> fun x -> Minute(x)
    let attemptDay = attempt pday
    let attemptHour = attempt phour
    let attemptMinute = attempt pminute
    let pduration = attemptDay <|> attemptHour <|> attemptMinute
    let manyduration = many1 pduration
    
    let buildDuration results =
        let mutable duration = { Days = 0; Hours = 0; Minutes = 0 }
        for r in results do
            duration <-
                match r with
                | Day d -> { duration with Days = d }
                | Hour h -> { duration with Hours = h }
                | Minute m -> { duration with Minutes = m }
        duration

    let parse (value: string): Result<Interval, string> =
        let result = run manyduration value
        match result with 
        | Success (u, _, _) -> 
            buildDuration u |> Core.Result<_,_>.Ok 
        | Failure (_, _, _) -> Core.Result<_,_>.Error $"'{value}' cannot be parsed"