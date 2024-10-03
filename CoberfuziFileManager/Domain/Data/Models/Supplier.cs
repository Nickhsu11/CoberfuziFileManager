using System.ComponentModel.DataAnnotations.Schema;

namespace CoberfuziFileManager.Models;

public class Supplier : Entity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int SupplierID { get; set; }
}