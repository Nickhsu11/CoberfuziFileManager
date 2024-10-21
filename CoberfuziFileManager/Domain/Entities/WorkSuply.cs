using System.ComponentModel.DataAnnotations;

namespace CoberfuziFileManager.Models;

public class WorkSuply
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int WorkID { get; set; }
    public Work Work { get; set; }
    
    [Required]
    public int SupplyID { get; set; }
    public Supply Supply { get; set; }
    
    public string unit { get; set; }
    
    public int QuantityUsed { get; set; }
}