using CurrencyExchange.Models;
using System.Text.Json;

namespace CurrencyExchange.Interfaces
{
    public class CurrencyConverter : ICurrencyConverter
    {
        //public decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
        //{
        //    if (fromCurrency.Equals("INR", StringComparison.OrdinalIgnoreCase) && toCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase))
        //    {                
        //        return amount / 80; // Assuming 1 USD = 80 INR
        //    }
        //    else if (fromCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase) && toCurrency.Equals("INR", StringComparison.OrdinalIgnoreCase))
        //    {                
        //        return amount * 80; // Assuming 1 USD = 80 INR
        //    }
        //    else
        //    {                
        //        throw new NotSupportedException("Conversion from " + fromCurrency + " to " + toCurrency + " is not supported.");
        //    }
        //}
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://v6.exchangerate-api.com/v6/ae4528f900bfc644e2f6f411/latest/";

        public CurrencyConverter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency)
        {
            string requestUrl = $"{BaseUrl}{fromCurrency}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve exchange rates.");
            }

            string jsonContent = await response.Content.ReadAsStringAsync();
            var exchangeRatesApiResponse = JsonSerializer.Deserialize<ExchangeRatesResponse>(jsonContent);

            if (exchangeRatesApiResponse.result != "success")
            {
                throw new Exception($"Failed to retrieve exchange rates: {exchangeRatesApiResponse.documentation}");
            }

            if (!exchangeRatesApiResponse.conversion_rates.TryGetValue(toCurrency.ToUpper(), out decimal conversionRate))
            {
                throw new Exception($"Conversion rate for {toCurrency} not found.");
            }

            return amount * conversionRate;
        }
    }

}

