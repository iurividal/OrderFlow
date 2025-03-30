namespace OrderFlow.Api.Models;

public class UserEntity
{
    public int Id { get; set; } // ID do usuário (sequencial auto-incremento)
    public string Username { get; set; } // Nome de usuário único
    public string FullName { get; set; } // Nome completo do usuário
    public string Email { get; set; } // Email único
    public string Password { get; set; } // Senha criptografada do usuário
    public DateTime CreatedAt { get; set; } // Data de criação
    public bool IsActive { get; set; } // Se o usuário está ativo ou não
    public int AccessLevel { get; set; } // Nível de acesso (1 para Usuário, 2 para Administrador)

    public AccessLevelEntity AccessLevelNavigation { get; set; } // Relacionamento com o nível de acesso
}