﻿using BO;

namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone Milestone { get; }
    public IEngineerInList EngineerInList { get; }
    public ITaskInEngineer TaskInEngineer { get; }
    public ITaskInList TaskInList { get; }
    public IEngineerInTask EngineerInTask { get; }
}
