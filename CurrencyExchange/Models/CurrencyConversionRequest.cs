namespace CurrencyExchange.Models
{
    public class CurrencyConversionRequest
    {
        public decimal Amount { get; set; }
        public string? FromCurrency { get; set; }
        public string? ToCurrency { get; set; }
    }
}
