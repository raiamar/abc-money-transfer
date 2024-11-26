using ABC.DTOs.Transaction;
using ABC.Repositories.Interface;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

public class ExchangeRateService : IExchangeRateService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ExchangeRateService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<TransactionResponseDto> GetExchangeRatesAsync(string fromDate, string toDate)
    {
        var currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
        var defaultFromDate = "2024-06-12";

        fromDate = string.IsNullOrWhiteSpace(fromDate) ? defaultFromDate : fromDate;
        toDate = string.IsNullOrWhiteSpace(toDate) ? currentDate : toDate;

        var client = _httpClientFactory.CreateClient();
        var url = $"https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from={fromDate}&to={toDate}";

        try
        {
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API call failed with status code {response.StatusCode}.");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                throw new InvalidOperationException("Received empty response from the API.");
            }
            var exchangeRates = JsonConvert.DeserializeObject<TransactionResponseDto>(responseContent);

            if (exchangeRates == null)
            {
                throw new JsonException("Error deserializing API response.");
            }

            return exchangeRates;
        }
        catch
        {
            throw;
            //return new TransactionResponseDto
            //{
            //    Payload = new List<Payload>(),
            //};
        }
    }
}
