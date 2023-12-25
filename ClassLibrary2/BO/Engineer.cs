using DO;

namespace BO;

public class Engineer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public BO.TaskInEngineer? Task {get; set;}
}
