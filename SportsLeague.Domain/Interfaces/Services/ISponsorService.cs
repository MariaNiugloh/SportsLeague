using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services;

public interface ISponsorService
{
    Task<IEnumerable<Sponsor>> GetAllAsync();
    Task<Sponsor?> GetByIdAsync(int id);
    Task<Sponsor> CreateAsync(Sponsor sponsor);
    Task UpdateAsync(int id, Sponsor sponsor);
    Task DeleteAsync(int id);
    Task AddSponsorToTournament(int sponsorId, int tournamentId, decimal contractAmount);
    Task<IEnumerable<TournamentSponsor>> GetTournamentsBySponsorId(int sponsorId);
    Task RemoveSponsorFromTournament(int sponsorId, int tournamentId);
}