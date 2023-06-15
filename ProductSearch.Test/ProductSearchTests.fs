module Tests

open Expecto
open System
open FsCheck
open Expecto.TestApi
open ProductSearch



open AcceptanceData

let makeProductTests (label:string) sutFactory =  

    testListWithEnv $"Product Search - {label}" [
        testPropertyWithEnv "should return an empty list when the given list is empty" <| fun (sut:IProductSearch) (partner: PartnerContract, queryDate: DateTime) ->
            Expect.isEmpty (sut.search partner.Id queryDate)

        testPropertyWithEnv "should include start dates equal to the queried date" <| fun (sut:IProductSearch) (partner: PartnerContract, date: DateTime) ->
            sut.savePartners [partner]
            let expected = [
                { 
                    Title = Guid.NewGuid() |> string
                    Artist = Guid.NewGuid() |> string
                    AllowedUsages = [partner.Usage]
                    StartDate = date
                    EndDate = None
                }
            ]
            sut.saveProducts expected

            let actual = sut.search partner.Id date
            Expect.equal actual expected "Start date should be inclusive"


        testListWithEnv "Acceptance Examples" [
            
            testWithEnv "Search for active music contracts" <| fun sut ->
                sut.saveProducts exampleProducts
                sut.savePartners examplePartners

                let expected = [
                    createProduct "Monkey Claw" "Black Mountain" ["digital download"] "02-01-2012" None
                    createProduct "Monkey Claw" "Motor Mouth" ["digital download"; "streaming"] "03-01-2011" None
                    createProduct "Tinie Tempah" "Frisky (Live from SoHo)" ["digital download"; "streaming"] "02-01-2012" None
                    createProduct "Tinie Tempah" "Miami 2 Ibiza" ["digital download"] "02-01-2012" None
                ]
                let actual = sut.search "ITunes" (DateTime.Parse("03-01-2012"))

                let likeness (products:Product list) = 
                    products 
                    |> List.map (fun p -> p.Title)
                    |> set

                Expect.equal (likeness actual) (likeness expected) ""

            testWithEnv "Search for active music contracts_2" <| fun sut ->
                sut.saveProducts exampleProducts
                sut.savePartners examplePartners

                let expected = [
                    createProduct "Monkey Claw" "Christmas Special" ["streaming"] "12-25-2012" (Some "12-31-2012")
                    createProduct "Monkey Claw" "Iron Horse" ["digital download"; "streaming"] "06-01-2012" None
                    createProduct "Monkey Claw" "Motor Mouth" ["digital download"; "streaming"] "03-01-2011" None
                    createProduct "Tinie Tempah" "Frisky (Live from SoHo)" ["digital download"; "streaming"] "02-01-2012" None
                ]
                let actual = sut.search "YouTube" (DateTime.Parse("12-27-2012"))

                let likeness (products:Product list) = 
                    products 
                    |> List.map (fun p -> p.Title)
                    |> set

                Expect.equal (likeness actual) (likeness expected) ""

            testWithEnv "Search for active music contracts_3" <| fun sut ->
                sut.saveProducts exampleProducts
                sut.savePartners examplePartners

                let expected = [
                    createProduct "Monkey Claw" "Motor Mouth" ["digital download"; "streaming"] "03-01-2011" None
                    createProduct "Tinie Tempah" "Frisky (Live from SoHo)" ["digital download"; "streaming"] "02-01-2012" None
                ]
                let actual = sut.search "YouTube" (DateTime.Parse("04-01-2012"))

                let likeness (products:Product list) = 
                    products 
                    |> List.map (fun p -> p.Title)
                    |> set

                Expect.equal (likeness actual) (likeness expected) ""
        ]
    ] sutFactory

    


let inMemoryEnvFactory = {
    setup = fun () -> 
        let mutable _products : Product list = []
        let mutable _partners : PartnerContract list = []
        let api : IProductSearch = {
            search = fun partner date -> 
                match ProductSearch.tryGetPartnerUsage _partners partner with
                | None -> []
                | Some usage->
                    ProductSearch.search _products usage date 
            savePartners = fun partners ->
                _partners <- partners
            saveProducts = fun products ->
                _products <- products
        }
        (api, ())
    cleanup = fun _ -> ()
}

[<Tests>]
let inMemorySearchTests = makeProductTests "InMemory" inMemoryEnvFactory

