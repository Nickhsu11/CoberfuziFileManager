using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoberfuziFileManager.Models;

public class Supply
{
    [Key]
    public int ID { get; set; }
    
    [Required]
    public int SupplyID { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int Stock { get; set; }
    
    [Required]
    public int SupplierID { get; set; }
    public Supplier Supplier { get; set; }

    public ICollection<Work> Works { get; set; } = new List<Work>();
}