using Newtonsoft.Json.Linq;
using OnlinerParser.Models;
using OnlinerParser.Services.Implementations;

namespace OnlinerParser.Services.Abstractions;

public class OnlinerParserService(
    OnlinerParserContext context, 
    IHttpClientFactory httpClientFactory
    ) 
    : IOnlinerParserService
{
    public async Task<List<Furniture>> ParseBeds()
    {
        var furniture = new List<Furniture>();
        
        var client = httpClientFactory.CreateClient();
        
        var url = $"https://catalog.onliner.by/sdapi/catalog.api/search/bed";
        
        var pageResponse = await client.GetAsync($"{url}");
        
        var content = await pageResponse.Content.ReadAsStringAsync();

        var jsonRoot = JObject.Parse(content);
        
        var pageItems = (JArray)jsonRoot["products"];

        foreach (var pageItem in pageItems!)
        {
            var imgUri = pageItem["images"]!["header"]!.Value<string>();
            using var response = await client.GetAsync($@"https:{imgUri}");
            var bytes = await response.Content.ReadAsByteArrayAsync();

            var desc = pageItem["description"]!.Value<string>();
            var b = desc.Split(",");
            var a = b[1].Split(" ");
            var size = a[1].Split("x");

            double.TryParse(size[0], out var w);
            
            double.TryParse(size[1], out var l);
            
            var fur = new Furniture
            {
                ProductId = pageItem["id"]!.Value<int>(),
                Name = pageItem["full_name"]!.Value<string>(),
                Length = l,
                Width = w,
                //Height = pageItem["price"]!["converted"]!["BYN"]!["amount"]!.Value<double>(),
                Price = pageItem["prices"]!["price_min"]!["amount"]!.Value<double>(),
                Link = pageItem["reviews"]!["html_url"]!.Value<string>(),
                FurnitureType = pageItem["name_prefix"]!.Value<string>(),
                Image = bytes
            };

            furniture.Add(fur);
        }
        
        return furniture;
    }
}