namespace PrimeService;

public class FlawedPrimeService : PrimeService
{
    public bool IsPrime(int candidate)
    {
        if (candidate < 2)
        {
            return false;
        }
        
        throw new NotImplementedException("Not fully implemented.");
    }
}