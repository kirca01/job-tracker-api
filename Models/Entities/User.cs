namespace JobTracker.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!; 
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
}