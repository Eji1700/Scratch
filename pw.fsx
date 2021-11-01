open System

type LoginModel = {Username: string; Password: string}

let Admins = [{Username="MJ";Password="M"}; {Username="Joe";Password="C"}] |> Set.ofList

let rec LoginRoutine() = 
    printfn "Welcome to VMS Version V!"
    printf "Enter your username to continue: "
    let usernameAttempt = Console.ReadLine()
    printf "Enter your password to continue: "
    let passwordAttempt = Console.ReadLine()
    let loginCredentials = {Username=usernameAttempt; Password=passwordAttempt}
    match Admins.Contains loginCredentials with
    | true -> printfn "You are logged in"
    | false -> LoginRoutine()