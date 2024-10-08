namespace Clinica.Infrastructure.Repositories;

public class TriagemRepository : ITriagemRepository
{
    private readonly ApplicationDbContext _context;

    public TriagemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<TriagemEntity>> GetAllAsync()
    {
        return await _context.Triagens.ToListAsync();
    }

    public async Task<TriagemEntity?> GetByIdAsync(int id, bool asNoTracking = false)
    {
        IQueryable<TriagemEntity> query = _context.Triagens
            .Include(t => t.Atendimento);

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        var triagem = await query.FirstOrDefaultAsync(t => t.Id == id);

        return triagem;
    }

    public async Task<ICollection<TriagemEntity>> GetEspecialidadeAtendimentoAsync(int id, bool asNoTracking = false)
    {
        IQueryable<TriagemEntity> query = _context.Triagens
            .Include(a => a.Especialidade)
            .Include(a => a.Atendimento);

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        var atendimento = await query.Where(a => a.Id == id).ToListAsync();

        return atendimento;
    }

    public async Task AddAsync(TriagemEntity triagem)
    {
        await _context.Triagens.AddAsync(triagem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TriagemEntity triagem)
    {
        _context.Triagens.Update(triagem);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        bool result = false;
        var triagem = await _context.Triagens.FindAsync(id);
        if (triagem != null)
        {
            _context.Triagens.Remove(triagem);
            await _context.SaveChangesAsync();
            result = true;
        }
        return result;
    }
}
