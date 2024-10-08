namespace Clinica.Api.IoC;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Repositórios

        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();
        services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
        services.AddScoped<ITriagemRepository, TriagemRepository>();

        // AutoMapper

        services.AddAutoMapper(typeof(EspecialidadeProfile));
        services.AddAutoMapper(typeof(PacienteProfile));
        services.AddAutoMapper(typeof(AtendimentoProfile));
        services.AddAutoMapper(typeof(TriagemProfile));

        // MediatR para CQRS

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(UpsertAtendimentoCommandHandler).Assembly,
            typeof(UpsertEspecialidadeCommandHandler).Assembly,
            typeof(UpsertPacienteCommandHandler).Assembly,
            typeof(UpsertTriagemCommandHandler).Assembly,
            typeof(GetEspecialidadeQueryHandler).Assembly,
            typeof(DeleteEspecialidadeCommandHandler).Assembly
        ));
    }
}
