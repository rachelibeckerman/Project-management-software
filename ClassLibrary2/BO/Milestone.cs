using System.Security.Cryptography.X509Certificates;

namespace BO;
/// <summary>
/// Milestone entity defenition
/// </summary>
public class Milestone
{
    public int Id { get; init; } 
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public BO.Status Status { get; set; }
    public DateTime? start { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? Deadline { get; set; } 
    public DateTime? Complete { get; set; }
    public Double? CompletionPercentage { get; set; }
    public string? Remarks { get; set; }
    public List<TaskInList>? Dependecies { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
