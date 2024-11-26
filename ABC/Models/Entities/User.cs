using Microsoft.AspNetCore.Identity;

namespace ABC.Models.Entities;

public class User : IdentityUser
{
    public string? Name { get; set; }
    public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
}
