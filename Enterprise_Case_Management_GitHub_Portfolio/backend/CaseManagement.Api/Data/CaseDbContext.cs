using CaseManagement.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace CaseManagement.Api.Data;

public class CaseDbContext(DbContextOptions<CaseDbContext> options) : DbContext(options)
{
    public DbSet<CaseRecord> Cases => Set<CaseRecord>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CaseRecord>().HasData(
            new CaseRecord { Id=1, Title="Review permit application", AssignedTo="Jordan Lee", Description="Verify submitted forms and supporting documents.", Priority="High", Status="Open", CreatedAtUtc=new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc) },
            new CaseRecord { Id=2, Title="Resolve service request", AssignedTo="Alex Rivera", Description="Coordinate follow-up with internal stakeholders.", Priority="Medium", Status="In Progress", CreatedAtUtc=new DateTime(2026, 10, 2, 12, 0, 0, DateTimeKind.Utc) },
            new CaseRecord { Id=3, Title="Archive completed review", AssignedTo="Taylor Kim", Description="Complete final quality checklist.", Priority="Low", Status="Closed", CreatedAtUtc=new DateTime(2026, 10, 3, 12, 0, 0, DateTimeKind.Utc) }
        );
    }
}
