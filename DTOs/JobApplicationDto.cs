namespace JobTracker.DTOs;
using JobTracker.Models.Entities;

public class CreateJobApplicationDto
{
    public string Company { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string? Notes { get; set; }
    public string? JobUrl { get; set; }
}

public class UpdateJobApplicationDto
{
    public string? Company { get; set; }
    public string? Position { get; set; }
    public string? Notes { get; set; }
    public string? JobUrl { get; set; } 
    public ApplicationStatus? Status { get; set; }
}

public class JobApplicationResponseDto
{
    public int Id { get; set; }
    public string Company { get; set; } = null!;
    public string Position { get; set; } = null!; 
    public string Status { get; set; } = null!;
    public DateTime AppliedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Notes { get; set; }
    public string? JobUrl { get; set; } 
}