using CaseManagement.Api.Data;
using CaseManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Api.Controllers;

[ApiController]
[Route("api/cases")]
public class CasesController(CaseDbContext db) : ControllerBase
{
    private static readonly string[] Statuses = ["Open", "In Progress", "Closed"];
    private static readonly string[] Priorities = ["Low", "Medium", "High"];

    [HttpGet]
    public async Task<ActionResult<List<CaseRecord>>> GetAll([FromQuery] string? status, [FromQuery] string? search)
    {
        IQueryable<CaseRecord> query = db.Cases.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(status) && status != "All") query = query.Where(c => c.Status == status);
        if (!string.IsNullOrWhiteSpace(search)) query = query.Where(c => c.Title.Contains(search) || c.AssignedTo.Contains(search));
        return Ok(await query.OrderByDescending(c => c.CreatedAtUtc).ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CaseRecord>> GetById(int id)
    {
        var item = await db.Cases.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<CaseRecord>> Create(CreateCaseRequest request)
    {
        if (!Priorities.Contains(request.Priority)) return BadRequest(new { message = "Invalid priority." });
        var item = new CaseRecord { Title=request.Title.Trim(), AssignedTo=request.AssignedTo.Trim(), Description=request.Description.Trim(), Priority=request.Priority };
        db.Cases.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id=item.Id }, item);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<CaseRecord>> UpdateStatus(int id, UpdateStatusRequest request)
    {
        if (!Statuses.Contains(request.Status)) return BadRequest(new { message = "Invalid status." });
        var item = await db.Cases.FindAsync(id);
        if (item is null) return NotFound();
        item.Status = request.Status;
        await db.SaveChangesAsync();
        return Ok(item);
    }
}
