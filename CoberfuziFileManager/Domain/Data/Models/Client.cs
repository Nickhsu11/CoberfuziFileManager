using System.ComponentModel.DataAnnotations.Schema;

namespace CoberfuziFileManager.Models;

public class Client : Entity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int ClientId { get; set; }
}