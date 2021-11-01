let highAndLow (str : string) = 
    let nums =
        str.Split(' ')
        |> Array.map int
    let low = nums |> Array.min 
    let high = nums |> Array.max 
    high.ToString() + " " + low.ToString()


highAndLow "1 2 3 4 5"
highAndLow "1 2 -3 4 5"
highAndLow "1 9 3 4 -5"