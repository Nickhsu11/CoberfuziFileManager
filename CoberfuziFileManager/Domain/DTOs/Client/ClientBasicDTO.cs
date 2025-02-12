namespace CoberfuziFileManager.Domain.DTOs;

public class ClientBasicDTO
{
    
    public string Name { get; set; }
    public string Nif { get; set; }
    public string PhoneNumber { get; set; }
    public int ClientID { get; set; }
    
    public override string ToString()
    {
        return $"Client ID: {ClientID} | " +
               $"Name: {Name} | " +
               $"Phone: {PhoneNumber} | " +
               $"Nif: {Nif} ";
    }
    
}