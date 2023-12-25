namespace BO;
/// <summary>
/// TaskInList entity defenition
/// </summary>
public class TaskInList
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public Status Status { get; set; }
}
