open System
open System.Globalization

let test = Guid.NewGuid()
let dates = ["20200202"; "lol"; "02/02/2020"; "stuff"; "02/02/20"; "testvalue"]
let TryParseToOption f =
    match f with 
    | true, r -> Some r
    | false, _ -> None

let parseDateWithFormat (stringDate:string) (format: string) =
    DateTime.TryParseExact(stringDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None) 
    |> TryParseToOption

let parseDateNew stringDate =
    ["yyyyMMdd"; "dd/MM/yyyy"; "dd/MM/yy"]
    |> List.tryPick(fun fmt ->
        parseDateWithFormat stringDate fmt)

let parseDates dates =
    dates
    |> List.map parseDateNew

let tryParseDate (formats: string list) stringDate =
    formats
    |> Seq.map (fun fmt -> DateTime.TryParseExact(stringDate,fmt,CultureInfo.InvariantCulture,DateTimeStyles.None))
    |> Seq.tryFind fst
    |> Option.map snd

let formats = ["yyyyMMdd"; "dd/MM/yyyy"; "dd/MM/yy"]

parseDates dates

dates
|> List.map (tryParseDate formats)

let rec movingAverages list = 
    match list with
    // if input is empty, return an empty list
    | [] -> []
    // otherwise process pairs of items from the input 
    | x::y::rest -> 
        let avg = (x+y)/2.0 
        //build the result by recursing the rest of the list
        avg :: movingAverages (y::rest)
    // for one item, return an empty list
    | [_] -> []

// test
movingAverages [1.0]
movingAverages [1.0; 2.0]
movingAverages [1.0; 2.0; 3.0]