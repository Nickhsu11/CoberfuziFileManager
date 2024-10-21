using CoberfuziFileManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CoberfuziFileManager.Data;

public class AppDbContext : DbContext
{
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Supplier> Suppliers{ get; set; }
    public DbSet<Work> Works { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Supply> Supplies { get; set; }
    public DbSet<WorkSuply> WorkSuplies { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=coberfuzi.sqlite");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        EntityModelSeed(modelBuilder);
        ClientModelSeed(modelBuilder);
        SupplierModelSeed(modelBuilder);
        
        WorkModelSeed(modelBuilder);
        SupplyModelSeed(modelBuilder);

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

        modelBuilder.Entity<Entity>()
            .HasIndex(u => u.Nif)
            .IsUnique();

    }

    private void ClientModelSeed(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Client>()
            .HasIndex(c => c.ClientId)
            .IsUnique();

        // Set's the relation ship between Client and Work
        // Beeing this Many Works can belong to one Client and
        // a Client only belongs to one work, and we can find the works
        // through the foreign key
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Works)
            .WithOne(w => w.Client)
            .HasForeignKey(w => w.ClientID);

    }

    private void SupplierModelSeed(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Supplier>()
            .HasIndex(s => s.SupplierID)
            .IsUnique();
        
        modelBuilder.Entity<Supplier>()
            .HasMany(s => s.Supplies)
            .WithOne(s => s.Supplier)
            .HasForeignKey(s => s.SupplierID);
        
    }

    private void WorkModelSeed(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Work>()
            .HasIndex(w => w.WorkID)
            .IsUnique();

        modelBuilder.Entity<Work>()
            .HasOne(w => w.Budget)
            .WithOne(b => b.Work)
            .HasForeignKey<Budget>(w => w.WorkID)
            .IsRequired();
        
    }

    private void BudgetModelSeed(ModelBuilder modelBuilder)
    {
        
    }
    
    private void SupplyModelSeed(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Supply>()
            .HasIndex(s => s.SupplierID)
            .IsUnique();
        
    }
    
    private void WorkSuplyModelSeed(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<WorkSuply>()
            .HasKey(ws => new { ws.WorkID, ws.SupplyID });

        modelBuilder.Entity<WorkSuply>()
            .HasOne(ws => ws.Work)
            .WithMany(w => w.WorkAndSupplies)
            .HasForeignKey(ws => ws.WorkID);

        modelBuilder.Entity<WorkSuply>()
            .HasOne(ws => ws.Supply)
            .WithMany(s => s.WorkAndSupplies)
            .HasForeignKey(ws => ws.SupplyID);
        
    }
    
}