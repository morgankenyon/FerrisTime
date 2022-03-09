# FerrisTime

![](https://img.shields.io/nuget/v/FerrisTime)

A simple library for parsing and extracting time durations from strings.

## Examples

```fsharp
open FerrisTime.Duration

//duration type
type Interval = { Days: int; Hours: int; Minutes: int }

//parse method
val parse: value: string -> Result<Interval,string>

let dayParse = parse "8d"
//Ok { Days = 8; Hours = 0; Minutes = 0 }

let hourParse = parse "20h"
//Ok { Days = 0; Hours = 20; Minutes = 0 }

let minuteParse = parse "60m"
//Ok { Days = 0; Hours = 0; Minutes = 60 }

let allParse = parse "1d 3h 60m"
//Ok { Days = 1; Hours = 3; Minutes = 60 }

let badParse = parse "23"
//Error "'23' cannot be parsed"
```
