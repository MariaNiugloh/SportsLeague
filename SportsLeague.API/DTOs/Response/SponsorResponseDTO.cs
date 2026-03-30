namespace SportsLeague.API.DTOs.Response;

using SportsLeague.Domain.Enums;

public class SponsorResponseDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public string? Phone { get; set; }

    public string? WebsiteUrl { get; set; }

    public SponsorCategory Category { get; set; }

    public DateTime CreatedAt { get; set; }
}