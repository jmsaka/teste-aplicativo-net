namespace Clinica.Domain.Entities;

public class PacienteEntity
{
    public required int Id { get; set; }
    public required string Nome { get; set; }
    public required string Telefone { get; set; }
    public required string Sexo { get; set; }
    public string? Email { get; set; }
    public required ICollection<AtendimentoEntity> Atendimentos { get; set; }
}
