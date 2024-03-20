namespace OnlinerParser.Models;

public class Furniture
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    public string Name { get; set; }
    
    public double Length { get; set; }
    
    public double Width { get; set; }
    
    //public double Height { get; set; }
    
    public double Price { get; set; }
    
    public string Link { get; set; }
    
    public string FurnitureType { get; set; }
    
    public byte[] Image { get; set; }
}