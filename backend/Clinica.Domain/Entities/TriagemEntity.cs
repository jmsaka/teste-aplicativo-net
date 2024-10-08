namespace Clinica.Domain.Entities;

public class TriagemEntity
{
    public int Id { get; set; }
    public required string Sintoma { get; set; }
    public required string PressaoArterial { get; set; }
    public float Peso { get; set; }
    public float Altura { get; set; }

    // Foreign Key
    public int EspecialidadeId { get; set; }
    public required EspecialidadeEntity Especialidade { get; set; }

    // Foreign Key
    public int AtendimentoId { get; set; }
    public required AtendimentoEntity Atendimento { get; set; }
}
