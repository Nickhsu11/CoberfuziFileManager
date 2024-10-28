namespace CoberfuziFileManager.Domain.DTOs.Supply;

public class SupplyBasicDTO
{
    public string Name { get; set; }
    
    public int SupplyID { get; set; }
    
    public string Units { get; set; }
    
    public double Stock { get; set; }
    
    public override string ToString()
    {
        return $"SupplyID: {SupplyID} | " +
               $"Name: {Name} | Stock : {Stock} {Units} ";
    }
}