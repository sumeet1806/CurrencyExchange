using CurrencyExchange.Interfaces;
using CurrencyExchange.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyConverter _converter;

    public CurrencyController(ICurrencyConverter converterS)
    {
        _converter = converterS;
    }

    [HttpPost("convert")]
    public async Task<IActionResult> ConvertCurrency([FromBody] CurrencyConversionRequest request)
    {
        try
        {
            decimal convertedAmount = await _converter.ConvertCurrencyAsync(request.Amount, request.FromCurrency, request.ToCurrency);
            return Ok(new CurrencyConversionResponse { ConvertedAmount = convertedAmount });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
