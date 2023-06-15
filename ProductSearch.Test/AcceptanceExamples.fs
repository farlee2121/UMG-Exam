module AcceptanceData

open ProductSearch
open System

let createProduct artist title usages startDate (endDate: string Option) : Product = 
    {
        Artist = artist
        Title = title
        AllowedUsages = usages
        StartDate = DateTime.Parse(startDate)
        EndDate = endDate |> Option.map DateTime.Parse
    }

let productExampleText = 
    @"Artist|Title|Usages|StartDate|EndDate
Tinie Tempah|Frisky (Live from SoHo)|digital download, streaming|02-01-2012|
Tinie Tempah|Miami 2 Ibiza|digital download|02-01-2012|
Tinie Tempah|Till I'm Gone|digital download|08-01-2012|
Monkey Claw|Black Mountain|digital download|02-01-2012|
Monkey Claw|Iron Horse|digital download, streaming|06-01-2012|
Monkey Claw|Motor Mouth|digital download, streaming|03-01-2011|
Monkey Claw|Christmas Special|streaming|12-25-2012|12-31-2012
"

let partnerExampleText =
    @"Partner|Usage
ITunes|digital download
YouTube|streaming
"


let exampleProducts = [
    createProduct "Tinie Tempah" "Frisky (Live from SoHo)" ["digital download"; "streaming"] "02-01-2012" None
    createProduct "Tinie Tempah" "Miami 2 Ibiza" ["digital download"] "02-01-2012" None
    createProduct "Tinie Tempah" "Till I'm Gone" ["digital download"] "08-01-2012" None
    createProduct "Monkey Claw" "Black Mountain" ["digital download"] "02-01-2012" None
    createProduct "Monkey Claw" "Iron Horse" ["digital download"; "streaming"] "06-01-2012" None
    createProduct "Monkey Claw" "Motor Mouth" ["digital download"; "streaming"] "03-01-2011" None
    createProduct "Monkey Claw" "Christmas Special" ["streaming"] "12-25-2012" (Some "12-31-2012")
]
let examplePartners = [
    {Id = "ITunes"; Usage = "digital download"}
    {Id = "YouTube"; Usage = "streaming"}
]