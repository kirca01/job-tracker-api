using JobTracker.DTOs;

namespace JobTracker.Services.Interfaces;

public interface IJobApplicationService
{
    Task<List<JobApplicationResponseDto>> GetAll(int userId, string? status);
    Task<JobApplicationResponseDto?> GetById(int id, int userId);
    Task<JobApplicationResponseDto> Create(CreateJobApplicationDto dto, int userId);
    Task<JobApplicationResponseDto?> Update(int id, UpdateJobApplicationDto dto, int userId);
    Task<bool> Delete(int id, int userId);
    Task<StatsDto> GetStats(int userId);
}