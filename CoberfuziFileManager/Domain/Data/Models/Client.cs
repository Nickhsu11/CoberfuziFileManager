using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoberfuziFileManager.Models;

public class Client : Entity
{
    public int ClientId { get; set; }
}