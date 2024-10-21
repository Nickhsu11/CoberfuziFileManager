namespace CoberfuziFileManager.Domain.DTOs.WorkSuply;

public class WorkSuplyCompleteDTO
{
    public int WorkID { get; set; }
    public int SupplyID { get; set; }
    public int QuantityUsed { get; set; }
    public string Unit { get; set; }
}