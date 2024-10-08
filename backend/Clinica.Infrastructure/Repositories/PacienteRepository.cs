namespace Clinica.Infrastructure.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly ApplicationDbContext _context;

    public PacienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PacienteEntity?> GetByIdAsync(int id, bool asNoTracking = false)
    {
        IQueryable<PacienteEntity> query = _context.Pacientes;

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        var paciente = await query.FirstOrDefaultAsync(p => p.Id == id);

        return paciente;
    }

    public async Task<ICollection<AtendimentoEntity>> GetPacienteAtendimentoAsync(int id, bool asNoTracking = false)
    {
        IQueryable<AtendimentoEntity> query = _context.Atendimentos
            .Include(a => a.Paciente)
            .Include(a => a.Triagem);

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        var atendimento = await query.Where(a => a.PacienteId == id).ToListAsync();

        return atendimento;
    }

    public async Task<ICollection<PacienteEntity>> GetAllAsync()
    {
        return await _context.Pacientes.ToListAsync();
    }

    public async Task AddAsync(PacienteEntity paciente)
    {
        await _context.Pacientes.AddAsync(paciente);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PacienteEntity paciente)
    {
        _context.Pacientes.Update(paciente);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        bool result = false;
        var paciente = await GetByIdAsync(id);
        if (paciente != null)
        {
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            result = true;
        }
        return result;
    }
}
