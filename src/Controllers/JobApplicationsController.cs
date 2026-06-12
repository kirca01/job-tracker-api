using System.Security.Claims;
using JobTracker.DTOs;
using JobTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JobApplicationsController : ControllerBase
{
    private readonly IJobApplicationService _service;

    public JobApplicationsController(IJobApplicationService service)
    {
        _service = service;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status)
    {
        var result = await _service.GetAll(GetUserId(), status);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetById(id, GetUserId());
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateJobApplicationDto dto)
    {
        var result = await _service.Create(dto, GetUserId());
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateJobApplicationDto dto)
    {
        var result = await _service.Update(id, dto, GetUserId());
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.Delete(id, GetUserId());
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var result = await _service.GetStats(GetUserId());
        return Ok(result);
    }

}