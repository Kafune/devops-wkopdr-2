using PrimeService.DAL.Models;

namespace PrimeService.DAL.Repositories;

public class PrimeRepository
{
    private readonly PrimeContext _primeContext;

    public PrimeRepository()
    {
        //TODO: Dependency Injection
        _primeContext = new PrimeContext();
    }
    
    public void Save(int priemgetal)
    {
        _primeContext.Primes
            .Add(new Prime
            {
                Number = priemgetal
            });
        _primeContext.SaveChanges();
    }
}