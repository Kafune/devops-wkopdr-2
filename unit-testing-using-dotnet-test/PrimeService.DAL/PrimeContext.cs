using Microsoft.EntityFrameworkCore;
using PrimeService.DAL.Models;

namespace PrimeService.DAL;

public class PrimeContext : DbContext
{
    public DbSet<Prime> Primes { get; set; }

    private string DbPath { get; }

    //TODO: Fetch values from .env
    public PrimeContext()
    {
        DbPath = "Host=postgres;Username=postgres;Password=secret;Database=primefinding";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(DbPath);
}