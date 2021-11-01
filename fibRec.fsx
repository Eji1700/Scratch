let rec fiboRec =
  function
  | 0L -> 0L
  | 1L -> 1L
  | n -> fiboRec (n-1L) + fiboRec (n-2L)

#time
for i in 0L..40L do
  printfn "fiboRec of %d => %d" i (fiboRec i)
#time

// iterative

let fiboIterative1 (n:bigint) = 
    Seq.init (int n) id
    |> Seq.fold (fun (n1,n2) items -> (n1+n2,n1)) (0L,1L)
    |> fst

#time
for i in 0I..180I do
  printfn "fiboIterative1 of %A => %A" i (fiboIterative1 i)
#time

fiboIterative1 2200I

// tail recursive

let fib n =
  let rec loop (n1,n2) i =
    printfn "%A" n1
    if i < n
    then loop (n1+n2,n1) (i+1I)
    else n1
  loop (0I,1I) 0I

#time
for i in 0I..180I do
  printfn "fib of %A => %A" i (fib i)
#time

fib 2200I