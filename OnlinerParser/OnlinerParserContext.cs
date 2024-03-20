using Microsoft.EntityFrameworkCore;
using OnlinerParser.Models;

namespace OnlinerParser;

public class OnlinerParserContext : DbContext
{
    public OnlinerParserContext(DbContextOptions<OnlinerParserContext> options) 
        : base(options)
    {
        
    }
    
    public DbSet<Furniture> Furniture { get; set; }
}