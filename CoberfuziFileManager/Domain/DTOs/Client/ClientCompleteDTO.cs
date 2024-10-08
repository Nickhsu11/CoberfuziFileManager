using System;
using System.Collections.Generic;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.DTOs;

public class ClientCompleteDTO
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string PostCode { get; set; }
    public string Address { get; set; }
    public string Description { get; set;  }
    public int Nif { get; set; }
    public int ClientID { get; set; }
    public ICollection<WorkCompleteDTO> Works { get; set; } = new List<WorkCompleteDTO>();
    
}