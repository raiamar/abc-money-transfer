using ABC.DTOs.Transaction;
using ABC.Models.Entities;

namespace ABC.Repositories.Interface;

public interface ITransactionService
{
    Task AddTransactionAsync(Transaction transaction);
    Task<(IEnumerable<TransactionDto> Transactions, int TotalCount)> GetTransactionsAsync(TransactionFilter filter);
}
