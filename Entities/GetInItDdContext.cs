using Microsoft.EntityFrameworkCore;

namespace GetInItBackEnd.Entities;

public class GetInItDdContext : DbContext
{
    private const string ConnectionString = "Server=localhost;Database=GetInItDB;User Id=SA;Password=GetInIt1234;TrustServerCertificate=True;Encrypt=True;";

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<EmploymentType> EmploymentTypes { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Technology> Technologies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Table Address Configuration
        modelBuilder.Entity<Address>().Property(a => a.Country).IsRequired().HasMaxLength(25);
        modelBuilder.Entity<Address>().Property(a => a.City).IsRequired().HasMaxLength(25);
        modelBuilder.Entity<Address>().Property(a => a.Street).IsRequired().HasMaxLength(25);
        modelBuilder.Entity<Address>().Property(a => a.BuildingNumber).IsRequired().HasMaxLength(25);
        modelBuilder.Entity<Address>().Property(a => a.PostalCode).IsRequired().HasMaxLength(25);
        //Table Company Configuration
        modelBuilder.Entity<Company>().Property(c => c.Name).IsRequired().HasMaxLength(25);
        modelBuilder.Entity<Company>().Property(c => c.Password).IsRequired().HasMaxLength(25);
        modelBuilder.Entity<Company>().Property(c => c.Nip).IsRequired();
        modelBuilder.Entity<Company>().Property(c => c.Regon).IsRequired();
        //Table EmploymentType Configuration
        modelBuilder.Entity<EmploymentType>().Property(e => e.Description).IsRequired().HasMaxLength(25);
        //Table JobApplication configuration
        modelBuilder.Entity<JobApplication>().Property(j => j.Name).IsRequired().HasMaxLength(25);
        modelBuilder.Entity<JobApplication>().Property(j => j.LastName).IsRequired().HasMaxLength(25);
        modelBuilder.Entity<JobApplication>().Property(j => j.Message).IsRequired().HasMaxLength(200);
        //Table Offer Configuration
        modelBuilder.Entity<Offer>().Property(o => o.Name).IsRequired().HasMaxLength(25);
        modelBuilder.Entity<Offer>().Property(o => o.Description).IsRequired().HasMaxLength(50);
        //Table Payment Configuration
        modelBuilder.Entity<Payment>().Property(p => p.PaymentDate).IsRequired();
        modelBuilder.Entity<Payment>().Property(p => p.Invoice).IsRequired();
        modelBuilder.Entity<Payment>().Property(p => p.Amount).IsRequired();
        //Technology Table Configuration
        modelBuilder.Entity<Technology>().Property(t => t.Language).IsRequired().HasMaxLength(25);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }
    
}