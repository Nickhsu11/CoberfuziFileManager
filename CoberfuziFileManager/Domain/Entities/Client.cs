using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoberfuziFileManager.Models;

public class Client : Entity
{
    [Required]
    public int ClientId { get; set; }
    
    // One - To Many rellationship : A Client have multiple works
    public ICollection<Work> Works { get; set; } = new List<Work>();

}