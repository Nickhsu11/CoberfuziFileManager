using System;
using System.ComponentModel.DataAnnotations;

namespace CoberfuziFileManager.Models;

public class Budget
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int BudgetId { get; set; }
    
    [Required]
    public long Value { get; set; }
    
    public DateTime SendingDate { get; set; }
    
    [Required]
    public int WorkID { get; set; }
    public Work Work { get; set; }
    
}