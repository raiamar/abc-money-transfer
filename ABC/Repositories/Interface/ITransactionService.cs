using ABC.Models.Entities;

namespace ABC.Repositories.Interface;

public interface ITransactionService
{
    Task AddTransactionAsync(Transaction transaction);
}
