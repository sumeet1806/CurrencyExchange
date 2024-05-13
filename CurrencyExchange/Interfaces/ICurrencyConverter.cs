namespace CurrencyExchange.Interfaces
{
    public interface ICurrencyConverter
    {
        Task<decimal> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency);
    }
}
