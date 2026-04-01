using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System.Linq;

namespace SportsLeague.Domain.Services;

public class SponsorService : ISponsorService
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
    private readonly ITournamentRepository _tournamentRepository;

    public SponsorService(
    ISponsorRepository sponsorRepository,
    ITournamentSponsorRepository tournamentSponsorRepository,
    ITournamentRepository tournamentRepository)
    {
        _sponsorRepository = sponsorRepository;
        _tournamentSponsorRepository = tournamentSponsorRepository;
        _tournamentRepository = tournamentRepository;
    }

    public async Task<IEnumerable<Sponsor>> GetAllAsync()
    {
        return await _sponsorRepository.GetAllAsync();
    }

    public async Task<Sponsor?> GetByIdAsync(int id)
    {
        return await _sponsorRepository.GetByIdAsync(id);
    }

    public async Task<Sponsor> CreateAsync(Sponsor sponsor)
    {
        if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
            throw new Exception("Sponsor name already exists");

        return await _sponsorRepository.CreateAsync(sponsor);
    }

    public async Task UpdateAsync(int id, Sponsor sponsor)
    {
        var existing = await _sponsorRepository.GetByIdAsync(id);

        if (existing == null)
            throw new Exception("Sponsor not found");

        if (existing.Name != sponsor.Name &&
            await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
        {
            throw new Exception("Sponsor name already exists");
        }

        sponsor.Id = id;

        await _sponsorRepository.UpdateAsync(sponsor);
    }

    public async Task DeleteAsync(int id)
    {
        await _sponsorRepository.DeleteAsync(id);
    }

    public async Task AddSponsorToTournament(int sponsorId, int tournamentId, decimal contractAmount)
    {
        var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
        if (sponsor == null)
            throw new Exception("Sponsor not found");

        var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
        if (tournament == null)
            throw new Exception("Tournament not found");

        if (contractAmount <= 0)
            throw new Exception("Contract amount must be greater than 0");

        var exists = await _tournamentSponsorRepository.ExistsAsync(tournamentId, sponsorId);
        if (exists)
            throw new Exception("Sponsor already linked to this tournament");

        var entity = new TournamentSponsor
        {
            SponsorId = sponsorId,
            TournamentId = tournamentId,
            ContractAmount = contractAmount,
            JoinedAt = DateTime.UtcNow
        };

        await _tournamentSponsorRepository.CreateAsync(entity);
    }

    public async Task<IEnumerable<TournamentSponsor>> GetTournamentsBySponsorId(int sponsorId)
    {
        var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
        if (sponsor == null)
            throw new Exception("Sponsor not found");

        var all = await _tournamentSponsorRepository.GetAllAsync();

        return all.Where(x => x.SponsorId == sponsorId);
    }

    public async Task RemoveSponsorFromTournament(int sponsorId, int tournamentId)
    {
        var all = await _tournamentSponsorRepository.GetAllAsync();

        var entity = all.FirstOrDefault(x =>
            x.SponsorId == sponsorId && x.TournamentId == tournamentId);

        if (entity == null)
            throw new Exception("Relationship not found");

        await _tournamentSponsorRepository.DeleteAsync(entity.Id);
    }
}