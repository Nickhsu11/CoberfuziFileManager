using System;

namespace CoberfuziFileManager.Models;

public class Work
{
    public int Id { get; set; }
    public int WorkID { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}