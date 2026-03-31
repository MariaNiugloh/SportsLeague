using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services;

public class SponsorService : ISponsorService
{
    private readonly ISponsorRepository _sponsorRepository;

    public SponsorService(ISponsorRepository sponsorRepository)
    {
        _sponsorRepository = sponsorRepository;
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
}