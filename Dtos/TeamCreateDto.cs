using System.ComponentModel.DataAnnotations;

namespace TeamApi.Dtos;

public class TeamCreateDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}
