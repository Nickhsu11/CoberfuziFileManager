using CoberfuziFileManager.Domain.DTOs.Budget;
using Tmds.DBus.Protocol;

namespace CoberfuziFileManager.Domain.DTOs;

public class WorkBasicDTO
{
    
    public int WorkID { get; set; }
    
    public string PostCode { get; set; }
    
    public string Address { get; set; }
    
    public BudgetCompleteDTO Budget { get; set; }
    
    public override string ToString()
    {
        return $"WorkID: {WorkID} | PostCode: {PostCode} | Address: {Address} | Budget: {Budget}";
    }
}