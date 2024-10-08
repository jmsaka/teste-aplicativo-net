namespace Clinica.Domain.Interfaces;

public interface ITriagemRepository
{
    Task<ICollection<TriagemEntity>> GetAllAsync();
    Task<TriagemEntity?> GetByIdAsync(int id, bool asNoTracking = false);
    Task<ICollection<TriagemEntity>> GetEspecialidadeAtendimentoAsync(int id, bool asNoTracking = false);
    Task AddAsync(TriagemEntity triagem);
    Task UpdateAsync(TriagemEntity triagem);
    Task<bool> DeleteAsync(int id);
}
