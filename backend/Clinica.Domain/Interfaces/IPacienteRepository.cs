namespace Clinica.Domain.Interfaces;

public interface IPacienteRepository
{
    Task<ICollection<PacienteEntity>> GetAllAsync();
    Task<PacienteEntity?> GetByIdAsync(int id, bool asNoTracking = false);
    Task<ICollection<AtendimentoEntity>> GetPacienteAtendimentoAsync(int id, bool asNoTracking = false);
    Task AddAsync(PacienteEntity paciente);
    Task UpdateAsync(PacienteEntity paciente);
    Task<bool> DeleteAsync(int id);
}
