namespace JobTracker.DTOs;

public class StatsDto
{
    public int Total { get; set; }
    public int Applied { get; set; }
    public int Interview { get; set; }
    public int Offer { get; set; }
    public int Rejected { get; set; }
    public double InterviewRate { get; set; }
    public double OfferRate { get; set; }
}