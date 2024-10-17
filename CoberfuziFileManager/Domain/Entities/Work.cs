using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoberfuziFileManager.Models;

public class Work
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int WorkID { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string PostCode { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    // Foreign Key to the Client
    [Required]
    public int ClientID { get; set; }
    public Client Client { get; set; }
    
    public Budget Budget { get; set; }
    
    public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}