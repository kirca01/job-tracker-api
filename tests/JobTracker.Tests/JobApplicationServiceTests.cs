using FluentAssertions;
using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models.Entities;
using JobTracker.Services;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Tests;

public class JobApplicationServiceTests
{
    private AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Create_ShouldAddJobApplication()
    {
        var context = CreateInMemoryContext();
        var service = new JobApplicationService(context);
        var dto = new CreateJobApplicationDto
        {
            Company = "Google",
            Position = "Backend Developer"
        };

        var result = await service.Create(dto, userId: 1);

        result.Should().NotBeNull();
        result.Company.Should().Be("Google");
        result.Position.Should().Be("Backend Developer");
        result.Status.Should().Be("Applied");
    }

    [Fact]
    public async Task GetAll_ShouldReturnOnlyUserApplications()
    {
        var context = CreateInMemoryContext();
        var service = new JobApplicationService(context);

        await service.Create(new CreateJobApplicationDto { Company = "Google", Position = "Dev" }, userId: 1);
        await service.Create(new CreateJobApplicationDto { Company = "Microsoft", Position = "Dev" }, userId: 1);
        await service.Create(new CreateJobApplicationDto { Company = "Amazon", Position = "Dev" }, userId: 2);

        var result = await service.GetAll(userId: 1, status: null);

        result.Should().HaveCount(2);
        result.Should().AllSatisfy(a => a.Company.Should().NotBe("Amazon"));
    }

    [Fact]
    public async Task Delete_ShouldReturnFalse_WhenApplicationDoesNotBelongToUser()
    {
        var context = CreateInMemoryContext();
        var service = new JobApplicationService(context);

        var app = await service.Create(new CreateJobApplicationDto { Company = "Google", Position = "Dev" }, userId: 1);

        var result = await service.Delete(app.Id, userId: 2);

        result.Should().BeFalse();
    }
}