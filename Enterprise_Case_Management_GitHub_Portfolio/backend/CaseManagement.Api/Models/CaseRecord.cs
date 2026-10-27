using System.ComponentModel.DataAnnotations;
namespace CaseManagement.Api.Models;

public class CaseRecord
{
    public int Id { get; set; }
    [Required, StringLength(120, MinimumLength = 3)] public string Title { get; set; } = "";
    [StringLength(100)] public string AssignedTo { get; set; } = "";
    [StringLength(1000)] public string Description { get; set; } = "";
    [Required] public string Priority { get; set; } = "Medium";
    [Required] public string Status { get; set; } = "Open";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

public class CreateCaseRequest
{
    [Required, StringLength(120, MinimumLength = 3)] public string Title { get; set; } = "";
    [StringLength(100)] public string AssignedTo { get; set; } = "";
    [StringLength(1000)] public string Description { get; set; } = "";
    [Required] public string Priority { get; set; } = "Medium";
}

public class UpdateStatusRequest
{
    [Required] public string Status { get; set; } = "";
}
