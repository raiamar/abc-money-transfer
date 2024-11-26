namespace ABC.Models.Common;

public abstract class PaginationValue
{
    public int PageNumber { get; set; } = 1; 
    public int PageSize { get; set; } = 10;
}
