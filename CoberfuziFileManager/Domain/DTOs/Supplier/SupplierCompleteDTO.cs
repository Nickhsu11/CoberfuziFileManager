using System;
using System.Collections;
using System.Collections.Generic;
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
    
}