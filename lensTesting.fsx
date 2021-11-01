type Lens<'a, 'b> = {
    Get: 'a -> 'b
    Set: 'b -> 'a -> 'a
} with 
    member l.Update f a =
        let value = l.Get a
        let newValue = f value
        l.Set newValue a

let inline (>>|) (l1: Lens<_,_>) (l2: Lens<_,_>) = 
    { Get = l1.Get >> l2.Get 
      Set = l2.Set >> l1.Update }

let inline (+=) (l: Lens<_,_>) v = l.Update ((+) v)

type Car = { 
    Make: string 
    Model: string 
    Mileage: int 
} with
    static member mileage =
        { Get = fun (c: Car) -> c.Mileage
          Set = fun v (x: Car) -> { x with Mileage = v} }
    static member model =
        { Get = fun (c: Car) -> c.Model
          Set = fun v (x: Car) -> { x with Model = v} }
    static member make =
        { Get = fun (c: Car) -> c.Make
          Set = fun v (x: Car) -> { x with Make = v} }

type Editor = { 
    Name: string 
    Salary: int 
    Car: Car 
} with
    static member car =
        { Get = fun (x:Editor) -> x.Car
          Set = fun v (x:Editor) -> { x with Car = v} } 
    static member salary =
        { Get = fun (e: Editor) -> e.Salary
          Set = fun v (x: Editor) -> { x with Salary = v} }
    static member name =
        { Get = fun (e: Editor) -> e.Name
          Set = fun v (x: Editor) -> { x with Name = v} }

type Book = { 
    Name: string 
    Author: string 
    Editor: Editor 
} with
    static member editor =
        { Get = fun (x:Book) -> x.Editor
          Set = fun v (x:Book) -> { x with Editor = v} }
    static member author =
        { Get = fun (e: Book) -> e.Author
          Set = fun v (x: Book) -> { x with Author = v} }
    static member name =
        { Get = fun (e: Book) -> e.Name
          Set = fun v (x: Book) -> { x with Name = v} }

let car = { Make = "Ford"; Model = "POS"; Mileage = 2 }
let editor = { Name = "Dude"; Salary = 12345; Car = car}
let book = { Name = "Ulysses"; Author = "Mr. SomeGuy"; Editor = editor}

let mileage = book.Editor.Car.Mileage

let book2 = { book with Editor =
                { book.Editor with Car =
                    { book.Editor.Car with Mileage = 1000 } } }

let test = book.Editor.Salary

let bookEditorCarMileage = Book.editor >>| Editor.car >>| Car.mileage

let mileage2 = book |> bookEditorCarMileage.Get
let book3 = book |> bookEditorCarMileage.Set 1000
let book4 = book |> bookEditorCarMileage += 1000

let test2 mile b = 
    b |> (Book.editor >>| Editor.car >>| Car.make) += mile