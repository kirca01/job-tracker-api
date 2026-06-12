using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models.Entities;
using JobTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Services;

public class JobApplicationService : IJobApplicationService
{
    private readonly AppDbContext _context;

    public JobApplicationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobApplicationResponseDto>> GetAll(int userId, string? status)
    {
        var query = _context.Applications.Where(a => a.UserId == userId);

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<ApplicationStatus> (status, true, out var parsedStatus))
            query = query.Where(a => a.Status == parsedStatus);

        return await query.OrderByDescending(a => a.AppliedAt).Select(a => ToDto(a)).ToListAsync();
    }

    public async Task<JobApplicationResponseDto?> GetById(int id, int userId)
    {
        var app = await _context.Applications.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        return app == null ? null : ToDto(app);
    }

    public async Task<JobApplicationResponseDto> Create(CreateJobApplicationDto dto, int userId)
    {
        var app = new JobApplication
        {
            Company = dto.Company,
            Position = dto.Position,
            Notes = dto.Notes,
            JobUrl = dto.JobUrl,
            UserId = userId
        };

        _context.Applications.Add(app);
        await _context.SaveChangesAsync();

        return ToDto(app);
    }

    public async Task<JobApplicationResponseDto?> Update(int id, UpdateJobApplicationDto dto, int userId)
    {
        var app = await _context.Applications.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (app == null) return null;

        if (dto.Company != null) app.Company = dto.Company;
        if (dto.Position != null) app.Position = dto.Position;
        if (dto.Notes != null) app.Notes = dto.Notes;
        if (dto.JobUrl != null) app.JobUrl = dto.JobUrl;
        if (dto.Status != null) app.Status = dto.Status.Value;
        app.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return ToDto(app);
    }

    public async Task<bool> Delete(int id, int userId)
    {
        var app = await _context.Applications.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (app == null) return false;

        _context.Applications.Remove(app);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<StatsDto> GetStats(int userId)
    {
        var apps = await _context.Applications.Where(a => a.UserId == userId).ToListAsync();

        var total = apps.Count;
        var applied = apps.Count(a => a.Status == ApplicationStatus.Applied);
        var interview = apps.Count(a => a.Status == ApplicationStatus.Interview);
        var offer = apps.Count(a => a.Status == ApplicationStatus.Offer);
        var rejected = apps.Count(a => a.Status == ApplicationStatus.Rejected);

        return new StatsDto
        {
            Total = total,
            Applied = applied,
            Interview = interview,
            Offer = offer,
            Rejected = rejected,
            InterviewRate = total == 0 ? 0 : Math.Round((double)interview / total * 100, 1),
            OfferRate = total == 0 ? 0 : Math.Round((double)offer / total * 100, 1)
        };
    }

    private static JobApplicationResponseDto ToDto(JobApplication app) => new()
    {
        Id = app.Id,
        Company = app.Company,
        Position = app.Position,
        Status = app.Status.ToString(),
        AppliedAt = app.AppliedAt,
        UpdatedAt = app.UpdatedAt,
        Notes = app.Notes,
        JobUrl = app.JobUrl
    };
}