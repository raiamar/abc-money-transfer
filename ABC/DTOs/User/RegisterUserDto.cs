using System.ComponentModel.DataAnnotations;

namespace ABC.DTOs.User;

public class RegisterUserDto
{
    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string UserName { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = default!;
}
