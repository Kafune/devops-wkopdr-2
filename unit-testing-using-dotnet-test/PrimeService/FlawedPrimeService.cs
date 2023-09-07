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
    
    public string IsPrimeText(int getal)
    {
        return IsPrime(getal)
            ? "Het opgevraagde getal is een priemgetal."
            : "Het opgevraagde getal is geen priemgetal.";
    }
}