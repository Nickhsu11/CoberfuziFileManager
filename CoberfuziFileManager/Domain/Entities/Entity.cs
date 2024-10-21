using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CoberfuziFileManager.Models;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int Nif { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string PostCode { get; set; }
    
    public string? Description { get; set; }
}