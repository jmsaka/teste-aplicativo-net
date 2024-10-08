using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Clinica.Infrastructure.Datas;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PacienteEntity> Pacientes { get; set; }
    public DbSet<AtendimentoEntity> Atendimentos { get; set; }
    public DbSet<TriagemEntity> Triagens { get; set; }
    public DbSet<EspecialidadeEntity> Especialidades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AtendimentoEntity>()
            .HasOne(a => a.Paciente)
            .WithMany(p => p.Atendimentos)
            .HasForeignKey(a => a.PacienteId);

        modelBuilder.Entity<TriagemEntity>()
            .HasOne(t => t.Atendimento)
            .WithOne(a => a.Triagem)
            .HasForeignKey<TriagemEntity>(t => t.AtendimentoId);

        modelBuilder.Entity<TriagemEntity>()
            .HasOne(t => t.Especialidade)
            .WithMany()
            .HasForeignKey(t => t.EspecialidadeId);
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Clinica.Api"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}