using AutoMapper;
using Clinica.Domain.DTO;

namespace Clinica.Infrastructure.Repositories;

public class AtendimentoRepository : IAtendimentoRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AtendimentoRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<AtendimentoDto>> GetAllEntityAsync()
    {
        var atendimentos = _mapper.Map<ICollection<AtendimentoDto>>(await _context.Atendimentos.ToListAsync());
        return atendimentos ?? [];
    }

    public async Task<ICollection<AtendimentoEntity>> GetAllAsync()
    {
        var atendimentos = await _context.Atendimentos
            .Include(a => a.Paciente)
            .Include(a => a.Triagem)
        .ToListAsync();

        return atendimentos ?? [];
    }

    public async Task<AtendimentoEntity?> GetByIdAsync(int id, bool asNoTracking = false)
    {
        IQueryable<AtendimentoEntity> query = _context.Atendimentos
            .Include(a => a.Paciente)
            .Include(a => a.Triagem);

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        var atendimento = await query.FirstOrDefaultAsync(a => a.Id == id);

        return atendimento;
    }

    public async Task<ICollection<AtendimentoEntity>> GetPacienteTriagemAsync(int id, bool asNoTracking = false)
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

    public async Task AddAsync(AtendimentoEntity atendimento)
    {
        await _context.Atendimentos.AddAsync(atendimento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AtendimentoEntity atendimento)
    {
        _context.Atendimentos.Update(atendimento);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        bool result = false;
        var atendimento = await _context.Atendimentos.FindAsync(id);
        if (atendimento != null)
        {
            _context.Atendimentos.Remove(atendimento);
            await _context.SaveChangesAsync();
            result = true;
        }
        return result;
    }
}
