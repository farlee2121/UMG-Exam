namespace ProductSearch

open System

type ProductUsage = string
type PartnerId = string

type Product = {
    Title: string
    Artist: string
    AllowedUsages: ProductUsage list
    StartDate: DateTime
    EndDate: DateTime option
}

type PartnerContract = {
    Id: PartnerId
    Usage: ProductUsage
}

type IProductSearch =  {
    search: PartnerId -> DateTime -> Product list
    saveProducts: Product list -> unit
    savePartners: PartnerContract list -> unit
}

module ProductSearch =
    let tryGetPartnerUsage partners partnerId = 
        partners 
        |> List.tryFind (fun partner -> partner.Id = partnerId)
        |> Option.map (fun matched -> matched.Usage)
    
    let search (products: Product list) (usage: ProductUsage)  (date: DateTime)  =
        products 
        |> List.where (fun product ->
            product.AllowedUsages |> List.contains usage
            && product.StartDate <= date 
            && (product.EndDate |> Option.map (fun ed -> date <= ed) |> Option.defaultValue true)
        )

    open FSharp.Data

    module String =
        let trim (s: string)= s.Trim()
        let split (separator:string) (s: string)= s.Split(separator) |> List.ofSeq

    type ProductFile = CsvProvider<
        Schema="Artist (string),Title (string),Usages (string),StartDate (date),EndDate (date?)",
        Separators="|",
        HasHeaders=false, 
        IgnoreErrors = true>

    let parseProducts text = 
        ProductFile.ParseRows (text)
        |> Array.map (fun (row)  ->
            {
                Artist = row.Artist
                Title = row.Title
                AllowedUsages = row.Usages |> String.split "," |> List.map String.trim
                StartDate = row.StartDate
                EndDate = Option.ofNullable row.EndDate
            }
        )
        |> List.ofArray

    type PartnerFile = CsvProvider<
        Separators="|",
        HasHeaders=false, 
        Schema="Partner (string),Usage (string)">

    let parsePartners text = 
        PartnerFile.ParseRows (text)
        |> Array.skip 1
        |> Array.map (fun (row)  ->
            {
                Id = row.Partner
                Usage = row.Usage
            }
        )
        |> List.ofArray