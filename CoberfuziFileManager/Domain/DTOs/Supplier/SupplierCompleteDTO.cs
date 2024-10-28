using System;
using System.Collections;
using System.Collections.Generic;
using CoberfuziFileManager.Domain.DTOs.Supply;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.DTOs;

public class SupplierCompleteDTO
{
    public string Name { get; set; }
    
    public string Phone { get; set; }
    
    public string Email { get; set; }

    public string PostCode { get; set; }

    public string Address { get; set; }
    
    public int Nif { get; set; }
    
    public int SupplierID { get; set; }

    public string? Description { get; set;  }
    
    public ICollection<SupplyCompleteDTO> Supplies { get; set; } = new List<SupplyCompleteDTO>();
    
    public override string ToString()
    {
        return $"SupplierID: {SupplierID} \n " +
               $"Name: {Name} \n " +
               $"Phone: {Phone} \n " +
               $"Email: {Email} \n " +
               $"Address: {Address} \n " +
               $"Description: {Description} \n ";
    }

    
}