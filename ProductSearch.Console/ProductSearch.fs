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

