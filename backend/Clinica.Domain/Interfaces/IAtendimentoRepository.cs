namespace Clinica.Domain.Interfaces;

public interface IAtendimentoRepository
{
    Task<ICollection<AtendimentoDto>> GetAllEntityAsync();
    Task<ICollection<AtendimentoEntity>> GetAllAsync();
    Task<AtendimentoEntity?> GetByIdAsync(int id, bool asNoTracking = false);
    Task<ICollection<AtendimentoEntity>> GetPacienteTriagemAsync(int id, bool asNoTracking = false);
    Task AddAsync(AtendimentoEntity atendimento);
    Task UpdateAsync(AtendimentoEntity atendimento);
    Task<bool> DeleteAsync(int id);
}
