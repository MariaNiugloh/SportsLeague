namespace SportsLeague.API.DTOs.Request;

using SportsLeague.Domain.Enums;

public class SponsorRequestDTO
{
    public string Name { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public string? Phone { get; set; }

    public string? WebsiteUrl { get; set; }

    public SponsorCategory Category { get; set; }
}