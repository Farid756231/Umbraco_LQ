using System.ComponentModel.DataAnnotations;

namespace umbraco_lingoquest.Dto;

public class Login
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}