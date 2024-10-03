using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoberfuziFileManager.Models;

public class Supplier : Entity
{
    public int SupplierID { get; set; }
}