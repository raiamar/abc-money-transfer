using System.ComponentModel.DataAnnotations;

namespace ABC.DTOs.User;

public class LoginUserDto
{
    [Required]
    public string UserName { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
