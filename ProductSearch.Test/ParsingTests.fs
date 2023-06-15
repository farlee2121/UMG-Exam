module ParsingTests

open Expecto
open ProductSearch


[<Tests>]
let parsingTests = testList "Parsing tests" [
    testCase "Can parse products example" <| fun () ->
        
        let actual = ProductSearch.parseProducts AcceptanceData.productExampleText

        Expect.equal actual AcceptanceData.exampleProducts ""

    testCase "Can parse partners example" <| fun () ->
        
        let actual = ProductSearch.parsePartners AcceptanceData.partnerExampleText

        Expect.equal actual AcceptanceData.examplePartners ""
]