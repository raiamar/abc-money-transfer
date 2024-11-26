using ABC.DTOs.Transaction;

namespace ABC.Repositories.Interface;

public interface IExchangeRateService
{
    Task<TransactionResponseDto> GetExchangeRatesAsync(string fromDate, string toDate);
}
