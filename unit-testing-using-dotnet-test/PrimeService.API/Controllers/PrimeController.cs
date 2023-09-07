using Microsoft.AspNetCore.Mvc;

namespace PrimeService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PrimeController : ControllerBase
{
    private readonly ILogger<PrimeController> _logger;
    private readonly PrimeService _primeService = new BardPrimeService();

    public PrimeController(ILogger<PrimeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("{getal}")]
    public string Get(int getal)
    {
        return _primeService.IsPrimeText(getal);
    }
}