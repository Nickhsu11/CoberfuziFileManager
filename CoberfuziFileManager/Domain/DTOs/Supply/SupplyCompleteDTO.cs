using System.Collections.Generic;

namespace CoberfuziFileManager.Domain.DTOs.Supply;

public class SupplyCompleteDTO
{
    
    public int SupplyId { get; set; }
    
    public string Name { get; set; }
    
    public double Stock { get; set; }
    
    public string Units { get; set; }
    
    public double Cost { get; set; }
    
    public int SupplierId { get; set; }
    
    public ICollection<WorkCompleteDTO> Works { get; set; } = new List<WorkCompleteDTO>();
}