module Program
open System.CommandLine
open System.IO
open System
open System.CommandLine.NamingConventionBinder
open ProductSearch


[<EntryPoint>]
let main args =
    let root = RootCommand("Search products with constraints")

    root.AddArgument (Argument<string>("products-file-path", "Path to file with products to search over"))
    root.AddArgument (Argument<string>("partners-file-path", "Path to partners music could be published to and their constraints"))
    root.AddArgument (Argument<string>("partner-id", "Find products able to publish to this partner e.g. ITunes or YouTube"))
    root.AddArgument (Argument<DateTime>("date", "Limit products to those available on this day e.g. 02-01-2012"))
    
    root.Handler <- CommandHandler.Create(
        (fun (productsFilePath: string) (partnersFilePath: string) (partnerId: string) (date: DateTime) ->
            Console.WriteLine($"Searching: \n product file: {productsFilePath} \n partner file: {partnersFilePath} \n partner: {partnerId} \n date: {date}\n")

            let mutable isInputValid = true
            if not (File.Exists(productsFilePath)) then
                printfn "Invalid product file path: %s" productsFilePath
                isInputValid <- false
            if not (File.Exists(partnersFilePath)) then
                printfn "Invalid partners file path: %s" partnersFilePath
                isInputValid <- false

            if isInputValid then
                let products = ProductSearch.parseProducts (File.ReadAllText productsFilePath)
                let partners = ProductSearch.parsePartners (File.ReadAllText partnersFilePath)
                let usage = ProductSearch.tryGetPartnerUsage partners partnerId

                match usage with
                | None ->
                    printfn "Cannot find partner: %s" partnerId
                | Some usage ->
                    let searchResults = ProductSearch.search products usage date

                    printfn "Artist | Title | Usages | StartDate | EndDate"

                    let formatDate (date:DateTime) = date.ToString("MM-dd-yyyy") 
                    for product in searchResults do
                        let prettyEndDate = 
                            match product.EndDate with
                            | None -> ""
                            | Some ed -> formatDate ed

                        printfn $"{product.Artist} | {product.Title} | {usage} | {formatDate product.StartDate} | {prettyEndDate}"

            0
    ))
    if args.Length = 0 
    then root.Invoke("--help")
    else root.Invoke args
    //root.Invoke [|".\products-example.txt"; ".\partners-example.txt"; "YouTube"; "02-01-2012"|]