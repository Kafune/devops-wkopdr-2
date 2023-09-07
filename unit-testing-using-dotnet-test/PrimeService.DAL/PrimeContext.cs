using Microsoft.EntityFrameworkCore;
using PrimeService.DAL.Models;

namespace PrimeService.DAL;

public class PrimeContext : DbContext
{
    public DbSet<Prime> Primes { get; set; }

    private string DbPath { get; }

    public PrimeContext()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DbPath = Path.Join(path, "primefinding.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}