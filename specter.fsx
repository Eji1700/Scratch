#r "nuget: Spectre.Console"
open Spectre.Console

type Table with 
    member this.MyBorder b =
        this.Border <- b
        this

type TableColumn with 
    member this.MyFooter (f:string) =
        this.Footer <- Markup(f)
        this
    member this.MyWidth w =
        this.Width <- w
        this
    member this.MyNoWrap() =
        this.NoWrap <- true
        this
    member this.MyPadding(s: int) =
        this.Padding <- s |> Padding
        this

type Cell =
    {   Color: string
        Value: int } 

type RowData = Cell []
 
type TableConfig = 
    {   Border: TableBorder
        BorderColor: Color
        Headers: TableColumn []
        Rows: string [] [] }

let board = 
    [|  [|0;0;0;0;0;0;0;0;0|]
        [|0;0;0;0;0;0;0;0;0|]
        [|0;0;0;0;0;0;0;0;0|]
        [|0;0;0;0;0;0;0;0;0|]
        [|0;0;0;0;0;0;0;0;0|]
        [|0;0;0;0;0;0;0;0;0|]
        [|0;0;0;0;0;0;0;0;0|]
        [|0;0;0;0;0;0;0;0;0|]
        [|0;0;0;0;0;0;0;0;0|] |] 
    |> array2D
    |> Array2D.map(fun n -> {Color="Black"; Value=n})

let headersFooters =
    [|  "1", "1"
        "2", "2"
        "3", "3"
        "4", "4"
        "5", "5"
        "6", "6"
        "7", "7"
        "8", "8"
        "9", "9" |]
    |> Array.map( fun (header, footer) ->
        TableColumn(header).MyFooter(footer).Centered().MyWidth(5).MyPadding(0))

let cellBlank = " "
let centerCell color value = $"[default on {color}]| {value} |[/]"
let rightCell color value = $"[default on {color}]| {value} [/][default on {color} bold]|[/]"
let leftCell color value = $"[default on {color} bold]|[/][default on {color}] {value} |[/]"
let boldCell color value = $"[default on {color} bold]{value}[/]"
let cellHorizontalBorder = $"[default on black]\u2014\u2014\u2014\u2014\u2014[/]"

let borderType cell = 
    [|  cell
        cell
        cell
        cell
        cell
        cell
        cell
        cell
        cell |]

let horizontalBorder color  = 
    let horizontalCell = boldCell color cellHorizontalBorder 
    borderType horizontalCell

let houseRow left center right =
    let lv = if left.Value = 0 then cellBlank else left.Value.ToString()
    let cv = if center.Value = 0 then cellBlank else center.Value.ToString()
    let rv = if right.Value = 0 then cellBlank else right.Value.ToString()
    [|  leftCell left.Color lv
        centerCell center.Color cv
        rightCell right.Color rv |]

let createRow (cells: RowData) = 
    [|  
        houseRow cells.[0] cells.[1] cells.[2]
        houseRow cells.[3] cells.[4] cells.[5]
        houseRow cells.[6] cells.[7] cells.[8] |]
    |> Array.concat

let rows = 
    [|  
        horizontalBorder "black"
        createRow board.[0, *]
        createRow board.[1, *]
        createRow board.[2, *]
        horizontalBorder "black"
        createRow board.[3, *]
        createRow board.[4, *]
        createRow board.[5, *]
        horizontalBorder "black"
        createRow board.[6, *]
        createRow board.[7, *]
        createRow board.[8, *]
        horizontalBorder "black"    
    |]

let config = 
    {   Border = TableBorder.Simple
        BorderColor = Color.Red
        Headers = headersFooters
        Rows = rows }

let table = 
    let newTable = 
        Table()
            .MyBorder(config.Border)
            .BorderColor(config.BorderColor)
            .AddColumns(config.Headers)
    config.Rows
    |> Array.map( fun row -> newTable.AddRow(row)  )
    |> ignore
    
    newTable

AnsiConsole.Write(table)