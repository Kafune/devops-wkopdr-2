namespace PrimeService;

using System;

public class BardPrimeService : PrimeService
{
    public bool IsPrime(int candidate)
    {
        if (candidate < 2)
        {
            return false;
        }

        for (var divisor = 2; divisor <= Math.Sqrt(candidate); divisor++)
        {
            if (candidate % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }

    public string IsPrimeText(int getal)
    {
        return IsPrime(getal)
            ? "Het opgevraagde getal is een priemgetal."
            : "Het opgevraagde getal is geen priemgetal.";
    }
}
