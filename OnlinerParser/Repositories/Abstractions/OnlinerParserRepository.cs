using OnlinerParser.Repositories.Implementations;
using OnlinerParser.Services.Implementations;

namespace OnlinerParser.Repositories.Abstractions;

public class OnlinerParserRepository(
    IOnlinerParserService service,
    OnlinerParserContext context
    ) 
    : IOnlinerParserRepository

{
    public async Task ParseOnliner()
    {
        var res = await service.ParseBeds();
        context.Furniture.AddRange(res);
        await context.SaveChangesAsync();
    }
}