namespace KlienLib

module Say =
    let hello name =
        printfn "Hello %s" name

module TimeIntervals =
    open FParsec
    type Interval = { Hours: int }

    type Unit = 
    | Hour of int
    

    let str s = pstring s
    let phour = pint32 .>> (pchar 'h' <|> pchar 'H') |>> fun x -> Hour(x)

    
    let parse value =
        let result = run phour value
        match result with 
        | Success (u, _, _) -> 
            match u with 
            | Hour x -> Some { Hours = x }
        | Failure (_, _, _) -> None