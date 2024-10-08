namespace Clinica.Domain.Entities;

public class AtendimentoEntity
{
    public required int Id { get; set; }
    public required int NumeroSequencial { get; set; }
    public required DateTime DataHoraChegada { get; set; }
    public required string Status { get; set; }

    // Foreign Key

    public required PacienteEntity Paciente { get; set; }

    public required TriagemEntity Triagem { get; set; }

    public required int PacienteId { get; set; }
}
