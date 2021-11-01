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
 
type TableConfig = 
    {   Border: TableBorder
        BorderColor: Color
        Headers: TableColumn []
        Rows: string [] [] }

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
        TableColumn(header).MyFooter(footer).Centered().MyWidth(5).MyPadding(0) )
let cellBlank = @"     "
let cellHorizontalBorder = @"-----"
let cellCenterBorder = @"  |  "
let cellValue = @"| 5 |"
let cellRightLine = @"  \  "
let cellLeftLine = @"  /  "
let coloredCell color cell = $"[default on {color}]{cell}[/]"


let coloredRow color cell =
    [|  coloredCell color cell 
        coloredCell color cell
        coloredCell color cell
        coloredCell color cell
        coloredCell color cell
        coloredCell color cell
        coloredCell color cell
        coloredCell color cell
        coloredCell color cell |]

let rows = 
    [|  coloredRow "black" cellHorizontalBorder
        coloredRow "black" cellValue
        coloredRow "black" cellHorizontalBorder
        coloredRow "black" cellValue
        coloredRow "black" cellHorizontalBorder
        coloredRow "black" cellValue
        coloredRow "black" cellHorizontalBorder
        coloredRow "black" cellValue
        coloredRow "black" cellHorizontalBorder
        coloredRow "black" cellValue
        coloredRow "black" cellHorizontalBorder
        coloredRow "black" cellValue
        coloredRow "black" cellHorizontalBorder |]

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