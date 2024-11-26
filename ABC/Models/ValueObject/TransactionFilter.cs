using ABC.Models.Common;

public class TransactionFilter : PaginationValue
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? Name { get; set; }
}
