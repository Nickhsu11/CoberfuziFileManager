using System;

namespace CoberfuziFileManager.Domain.DTOs;

public class WorkCompleteDTO
{
    
    public int WorkID { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }
    public int ClientID { get; set; }
    
}