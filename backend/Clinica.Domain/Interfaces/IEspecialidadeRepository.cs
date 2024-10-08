namespace Clinica.Domain.Interfaces;

public interface IEspecialidadeRepository
{
    Task<ICollection<EspecialidadeEntity>> GetAllAsync();
    Task<EspecialidadeEntity?> GetByIdAsync(int id, bool asNoTracking = false);
    Task<bool> TermExists(string term);
    Task AddAsync(EspecialidadeEntity especialidade);
    Task UpdateAsync(EspecialidadeEntity especialidade);
    Task<bool> DeleteAsync(int id);
}
