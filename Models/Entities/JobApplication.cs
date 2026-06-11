namespace JobTracker.Models.Entities;

public class JobApplication
{
    public int Id { get; set; }
    public string Company  { get; set; } = null!;
    public string Position { get; set; } = null!; 
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? Notes { get; set; }
    public string? JobUrl { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!; 
}

public enum ApplicationStatus
{
    Applied,
    Interview,
    Offer,
    Rejected
}