#r "nuget: FSharpPlus"
open System
open FSharpPlus
open FSharpPlus.Lens

type Person = 
    { Name: string
      DateOfBirth: DateTime }

module Person =
    let inline _name f p =
        f p.Name <&> fun x -> { p with Name = x }

type Book = 
    { Title: string
      Author: Person }

module Book =
    let inline _author f b =
        f b.Author <&> fun a -> { b with Author = a }

    let inline _authorName b = _author << Person._name <| b

let rayuela =
    { Book.Title = "Rayuela"
      Author = { Person.Name = "Julio Cortázar"
                 DateOfBirth = DateTime(1914, 8, 26) } }
    
// read book author name:
let authorName1 = view Book._authorName rayuela
//  you can also write the read operation as:
let authorName2 = rayuela ^. Book._authorName

// write value through a lens
let book1 = setl Book._authorName "William Shakespear" rayuela
// update value
let book2 = over Book._authorName String.toUpper rayuela