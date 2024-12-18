using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Applications.Services;
using ChessTournament.Infrastructure.Repositories;

namespace ChessTournament.API.DependencyInjection;

public static class ServiceExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ITournamentRepository, TournamentRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<ITournamentService, TournamentService>();

        return services;
    }
    
}