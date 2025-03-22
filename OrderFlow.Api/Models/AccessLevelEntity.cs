namespace OrderFlow.Api.Models;

public class AccessLevelEntity
{
    public int Id { get; set; }               // Id do nível de acesso
    public string Name { get; set; }          // Nome do nível de acesso (User, Administrator)
}