namespace Clinica.Domain.DTO;

public class AtendimentoDto
{
    public int Id { get; set; }
    public int NumeroSequencial { get; set; }
    public DateTime DataHoraChegada { get; set; }
    public required string Status { get; set; }
    public int PacienteId { get; set; }
    public required int TriagemId { get; set; }
}