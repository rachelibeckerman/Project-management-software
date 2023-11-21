namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    const string s_engineer = "engineers"; //XML Serializer

    public int Create(Engineer item)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        Engineer? foundValue = engineersList.Where(eng => eng?.Id == item.Id).FirstOrDefault();
        if (foundValue != null)
        {
            throw new DalAlreadyExistsException($"An Engineer with {item.Id} ID already exists.");
        }
        engineersList.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, s_engineer);
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Engineer?> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        Engineer? foundValue = engineersList.Where(eng => eng?.Id == id).First();
        if (foundValue == null)
        {
            throw new DalDoesNotExistException($"An Engineer with {id} ID does not exist.");
        }
        Engineer copyItem = foundValue with { Status = false };
        engineersList.RemoveAll(eng => eng?.Id == id);
        engineersList.Add(copyItem);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, s_engineer);
    }

    public Engineer? Read(int id)
    {
        List<Engineer?> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        Engineer? foundValue = engineersList.Where(eng => eng?.Id == id).FirstOrDefault();
        return foundValue != null ? foundValue : null;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer?> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        Engineer? foundValue = engineersList.Where(filter).First();
        return foundValue != null ? foundValue : null;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null) //stage 2
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        if (filter == null)
            return engineersList.Select(item => item);
        else
            return engineersList.Where(filter);

    }
    public void Update(Engineer item)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        Engineer? foundValue = engineersList.Where(eng => eng?.Id == item.Id).First();
        if (foundValue == null)
        {
            throw new DalDoesNotExistException($"An Engineer with {item.Id} ID does not exist.");
        }
        engineersList.RemoveAll(eng => eng?.Id == item.Id);
        engineersList.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, s_engineer);
    }

}
