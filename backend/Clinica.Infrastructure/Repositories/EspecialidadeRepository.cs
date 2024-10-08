namespace Clinica.Infrastructure.Repositories;

public class EspecialidadeRepository : IEspecialidadeRepository
{
    private readonly ApplicationDbContext _context;

    public EspecialidadeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<EspecialidadeEntity>> GetAllAsync()
    {
        return await _context.Especialidades.ToListAsync();
    }

    public async Task<EspecialidadeEntity?> GetByIdAsync(int id, bool asNoTracking = false)
    {
        IQueryable<EspecialidadeEntity> query = _context.Especialidades;

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        var especialidade = await query.FirstOrDefaultAsync(e => e.Id == id);

        return especialidade;
    }

    public async Task<bool> TermExists(string term)
    {
        var especialidade = await _context.Especialidades
            .Where(p => p.Nome.Equals(term))
            .FirstOrDefaultAsync();

        return especialidade != null;
    }

    public async Task AddAsync(EspecialidadeEntity especialidade)
    {
        await _context.Especialidades.AddAsync(especialidade);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EspecialidadeEntity especialidade)
    {
        _context.Especialidades.Update(especialidade);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        bool result = false;
        var especialidade = await _context.Especialidades.FindAsync(id);
        if (especialidade != null)
        {
            _context.Especialidades.Remove(especialidade);
            await _context.SaveChangesAsync();
            result = true;
        }
        return result;
    }
}
