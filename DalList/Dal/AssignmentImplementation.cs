

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

internal class AssignmentImplementation :IAssignment
{
    public int Create(Assignment item)
    {
        int newId = Dal.Config.NextAssignmentId;
        Assignment copy = new Assignment(newId, item.CallId, item.VolunteerId, item.TreatmentStartTime, item.TreatmentEndTime, item.TypeOfTreatmentEnding);
        DataSource.Assignments.Add(copy);
        return newId;
    }

    public void Delete(int id)
    {
        Assignment a = DataSource.Assignments.Find(element => element.Id == id);
        if (a == null)
            throw new Exception($"An object of type Volunteer with this Id {id} does not exist");
        DataSource.Assignments.Remove(a);
    }

    public void DeleteAll()
    {
        DataSource.Assignments.Clear();
    }

    public Assignment? Read(int id)
    {
        Assignment a = DataSource.Assignments.Find(element => element.Id == id);
        if (a != null)
            return a;
        return null;
    }

    public List<Assignment> ReadAll()
    {
        return new List<Assignment>(DataSource.Assignments);
    }

    public void Update(Assignment item)
    {
        Assignment a = DataSource.Assignments.Find(element => element.Id == item.Id);
        if (a == null)
            throw new Exception($"An object of type Volunteer with this Id {item.Id} does not exist");
        DataSource.Assignments.Remove(a);
        DataSource.Assignments.Add(item);
    }
   
}

