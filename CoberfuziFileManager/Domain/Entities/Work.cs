using System;
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
    
    // Navigation Proprety to the Client
    [Required]
    public Client Client { get; set; }
    
    
}