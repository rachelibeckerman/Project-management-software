﻿namespace DalTest;
using DalApi;
using DO;
using System.Data.Common;
/// <summary>
/// Initialization Data
/// </summary>
public static class Initialization
{
    private static IDal? s_dal; //stage 2


    private static readonly Random s_rand = new();


    private static void createEngineer()
    {
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;
        const int NUM_OF_LEVELS = 5;
        const int MIN_COST = 33;
        const int MAX_COST = 250;
        string[] EngineersNames = {
            "Dani Levi",
            "Eli Amar",
            "Yair Cohen",
            "Ariela Levin",
            "Dina Klein",
            "Shira Israelof"
        };
        foreach (var EngineerName in EngineersNames)
        {
            int id;
            do
            {
                id = s_rand.Next(MIN_ID, MAX_ID);
            }
            while (s_dal!.Engineer.Read(id) != null);
            string emailName = EngineerName.Replace(" ", "");
            string email = emailName + id/1000+"@company.org.il";
            int level = id % NUM_OF_LEVELS;
            double cost = (s_rand.NextDouble()+1)* s_rand.Next(MIN_COST, MAX_COST); 
            Engineer newEngineer = new Engineer(id,EngineerName, email,(EngineerExperience) level ,cost);
            s_dal!.Engineer.Create(newEngineer);
        }
    }
    private static void createTask()
    {
        const int NUM_OF_LEVELS = 5;
        const int MIN_DAYS = 30;
        const int MAX_DAYS = 100;
        for (int task = 0; task < 20; task++)
        {
            DateTime pastDate = new DateTime(2023, 1, 1);
            int rangeCreate = (DateTime.Today - pastDate).Days;
            DateTime createAtDate = pastDate.AddDays(s_rand.Next(rangeCreate));
            int complexityLevel = s_rand.Next() % NUM_OF_LEVELS;
            string description = createAtDate.ToString() + ((EngineerExperience)complexityLevel).ToString();
            bool milestone = false;
            string alias = createAtDate.Day.ToString() + ((EngineerExperience)complexityLevel).ToString()[0];
            DateTime futureDate = new DateTime(2025, 1, 1);
            int rangeStartDate = (futureDate - DateTime.Today).Days;
            DateTime startDate = DateTime.Today.AddDays(rangeStartDate);
            DateTime scheduledDate = startDate.AddDays(s_rand.Next(MIN_DAYS, MAX_DAYS)*complexityLevel);
            DateTime ForecastDate = startDate.AddDays(15);
            Task newTask = new Task(task,description,alias,milestone,createAtDate,startDate,scheduledDate,ForecastDate, null,null,null,null,null,(EngineerExperience)complexityLevel);
            s_dal!.Task.Create(newTask);
        }
    }

    private static void createDependency()
    {
        const int NUM_OF_TASK = 20;
        for (int dependency = 0; dependency < 40; dependency++)
        {
            int dependentTask = s_rand.Next(NUM_OF_TASK);
            int dependentOnTask = s_rand.Next(dependentTask);
            Dependency newDependency = new Dependency(dependency,dependentTask,dependentOnTask);
            s_dal!.Dependency.Create(newDependency);
        }
    }
    //public static void Do(IDal dal) //stage 2
    public static void Do() // stage 4
    {
        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2

        s_dal = DalApi.Factory.Get;//stage 4
        createEngineer();
        createTask();
        createDependency();
    }
}
