namespace CoberfuziFileManager.Domain.DTOs;

public class SupplierBasicDTO
{
    
    public string Name { get; set; }
    public string Nif { get; set; }
    public string PhoneNumber { get; set; }
    public string SupplierID { get; set; }
    
    public override string ToString()
    {
        return $"SupplierID: {SupplierID} | " +
               $"Name: {Name} | " +
               $"Phone: {PhoneNumber} | " +
               $"Nif: {Nif} ";
    }
    
}