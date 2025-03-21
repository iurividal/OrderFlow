using System.ComponentModel.DataAnnotations;

namespace OrderFlow.Shared;

public class UserModel
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }
        
    [Required(ErrorMessage = "Senha é obrigatória")]
    public string Password { get; set; }
        
    public bool RememberMe { get; set; }
}