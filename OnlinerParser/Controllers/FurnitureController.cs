using Microsoft.AspNetCore.Mvc;
using OnlinerParser.Repositories.Implementations;

namespace OnlinerParser.Controllers;

public class FurnitureController(
    IOnlinerParserRepository repository
    ) 
    : Controller
{
    
    
    [HttpGet("ParseOnliner")]
    public async Task<IActionResult> Parse()
    {
        await repository.ParseOnliner();
        return Ok();
    }
}