using System;
using System.Collections.Generic;
using CoberfuziFileManager.Domain.DTOs.Budget;
using CoberfuziFileManager.Domain.DTOs.Supply;

namespace CoberfuziFileManager.Domain.DTOs;

public class WorkCompleteDTO
{
    public int WorkID { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }
    public int ClientID { get; set; }
    public BudgetCompleteDTO Budget { get; set; }

    public ICollection<SupplyCompleteDTO> Supplies { get; set; } = new List<SupplyCompleteDTO>();

}