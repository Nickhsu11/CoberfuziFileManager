using System.Collections.Generic;

namespace CoberfuziFileManager.Domain.DTOs.Supply;

public class SupplyCompleteDTO
{
    
    public int SupplyId { get; set; }
    
    public string Name { get; set; }
    
    public int Stock { get; set; }
    
    public int SupplierId { get; set; }
    
    public ICollection<WorkCompleteDTO> Works { get; set; } = new List<WorkCompleteDTO>();
}