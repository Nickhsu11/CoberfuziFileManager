using CoberfuziFileManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CoberfuziFileManager.Data;

public class AppDbContext : DbContext
{
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Supplier> Suppliers{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=coberfuzi.sqlite");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        EntityModelSeed(modelBuilder);
        ClientModelSeed(modelBuilder);
        SupplierModelSeed(modelBuilder);
        
    }

    private void EntityModelSeed(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Entity>()
            .HasDiscriminator<string>("EntityType")
            .HasValue<Client>("Client")
            .HasValue<Supplier>("Supplier");
        
        modelBuilder.Entity<Entity>()
            .HasIndex(u => u.Name)
            .IsUnique();
        
    }

    private void ClientModelSeed(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Client>()
            .HasIndex(u => u.ClientId)
            .IsUnique();

        modelBuilder.Entity<Client>()
            .Property(u => u.ClientId)
            .ValueGeneratedOnAdd();

    }

    private void SupplierModelSeed(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Supplier>()
            .HasIndex(u => u.SupplierID)
            .IsUnique();

        modelBuilder.Entity<Supplier>()
            .Property(u => u.SupplierID)
            .ValueGeneratedOnAdd();

    }
    
}