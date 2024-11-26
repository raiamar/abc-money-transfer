using ABC.DTOs.Transaction;
using ABC.Mappers;
using ABC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _context;

    public TransactionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddTransactionAsync(ABC.Models.Entities.Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<(IEnumerable<TransactionDto> Transactions, int TotalCount)> GetTransactionsAsync(TransactionFilter filter)
    {
        var query = _context.Transactions.AsQueryable();

        if (filter.FromDate.HasValue)
            query = query.Where(t => t.CreatedAt.Date >= filter.FromDate.Value.Date);

        if (filter.ToDate.HasValue)
            query = query.Where(t => t.CreatedAt.Date <= filter.ToDate.Value.Date);

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(t =>
                t.SenderFirstName.Contains(filter.Name) ||
                t.ReceiverFirstName.Contains(filter.Name));
        }

        query = query.OrderBy(t => t.CreatedAt);

        var totalCount = await query.CountAsync();

        query = query.Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize);

        var transactions = await query.ToListAsync();
        return (transactions.Select(t => t.ToDto()), totalCount);
    }
}
