using System.ComponentModel.DataAnnotations;

namespace OrderFlow.Shared;

public class UserModel
{
    public string Id { get; set; }

    public string UserName { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    public string FullName { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Nível de acesso é obrigatório")]
    public string Role { get; set; }

    public DateTime? InactivatedAt { get; set; }

    
}