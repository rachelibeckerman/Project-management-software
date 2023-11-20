namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    const string s_dependency = "dependencies"; //Linq to XML

    static Dependency? getDependency(XElement d) =>
        d.ToIntNullable("Id") is null ? null : new Dependency()
        { 
            Id = (int)d.Element("Id")!,
            DependentTask = (int?)d.Element("DependentTask")!,
            DependentOnTask = (int?)d.Element("DependentTask")!
        };

    static IEnumerable<XElement> createDependencyElement(Dependency dependency)
    {
        yield return new XElement("Id", dependency.Id);
        if (dependency.DependentTask is not null)
            yield return new XElement("DependentTask", dependency.DependentTask);
        if (dependency.DependentOnTask is not null)
            yield return new XElement("DependentOnTask", dependency.DependentOnTask);
    }

  
    public int Create(Dependency item)
    { 
        XElement? dependenciesRootElem = XMLTools.LoadListFromXMLElement(s_dependency);
        dependenciesRootElem.Add(new XElement("Dependency", createDependencyElement(item)));
        XMLTools.SaveListToXMLElement(dependenciesRootElem, s_dependency);
        return item.Id;

    }

    public void Delete(int id)
    {
        XElement? dependenciesRootElem = XMLTools.LoadListFromXMLElement(s_dependency);
        (dependenciesRootElem.Elements().FirstOrDefault(dep => (int?)dep.Element("Id") == id) 
         ?? throw new Exception($"An Dependency with {id} id does not exist.")).Remove();
        XMLTools.SaveListToXMLElement(dependenciesRootElem, s_dependency);
    }

    public Dependency? Read(int id)
    {
        XElement? dependenciesRootElem = XMLTools.LoadListFromXMLElement(s_dependency)?.Elements()
                                        .FirstOrDefault(dep => dep.ToIntNullable("Id") == id);
        if(dependenciesRootElem == null)
            return null;
        return getDependency(dependenciesRootElem);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<Dependency?> dependenciesList = XMLTools.LoadListFromXMLElement(s_dependency).Elements().Select(getDependency).ToList();
        return dependenciesList?.Where(filter!).FirstOrDefault() ?? null;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        return filter is null ? XMLTools.LoadListFromXMLElement(s_dependency).Elements().Select(getDependency)
                            : XMLTools.LoadListFromXMLElement(s_dependency).Elements().Select(getDependency).Where(filter!);
    }

    public void Update(Dependency item)
    {
        Delete(item.Id);
        Create(item);
    }
}
