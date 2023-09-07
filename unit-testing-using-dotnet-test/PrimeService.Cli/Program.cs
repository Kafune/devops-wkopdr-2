namespace PrimeService.Cli;

using DAL.Repositories;

class Program
{
    private static readonly PrimeService PrimeService = new BardPrimeService();
    
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Geen getal opgegeven als argument. Geef een integer getal op om te checken.");
            return;
        }

        var conversionSucceeded = Int32.TryParse(args[0], out var getal);

        if (!conversionSucceeded)
        {
            Console.WriteLine("Opgegeven parameter is geen geldig getal.");
            return;
        }
        
        var isPriemgetal = PrimeService.IsPrime(getal);

        
        if (isPriemgetal)
        {
            Console.WriteLine($"{getal} is een priemgetal. Getal wordt opgeslagen!");
            return;
        }
        
        Console.WriteLine($"{getal} is geen priemgetal.");
    }
}