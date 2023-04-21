using Microsoft.EntityFrameworkCore;
using projekt.Models;



public class DatabaseDbContext : DbContext
{
    public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options)
    {
    }
    public DbSet<Pracownik> Pracownik { get; set; }
    public DbSet<Konto> Konto { get; set; }
    public DbSet<Praca> Praca { get; set; }
    public DbSet<WozekWidlowy> WozekWidlowy { get; set; }
    public DbSet<Magazyn> Magazyn { get; set; }
    public DbSet<Paczka> Paczka { get; set; }
    public DbSet<Firma> Firma { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Konto>()
        .HasOne(k => k.Pracownik)
        .WithOne(p => p.Konto)
        .HasForeignKey<Pracownik>(p => p.KontoId);

    }

}
    

