using OnlinerParser.Models;

namespace OnlinerParser.Services.Implementations;

public interface IOnlinerParserService
{
    Task<List<Furniture>> ParseBeds();
}