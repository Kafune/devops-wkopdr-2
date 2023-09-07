// See https://aka.ms/new-console-template for more information

namespace PrimeService.Cli;

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

        Int32.TryParse(args[0], out var getal);

        var isPriemgetal = PrimeService.IsPrime(getal) ? "Ja" : "Nee";
        
        Console.WriteLine($"Is {getal} een priemgetal? -> {isPriemgetal}");
    }
}