

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class AssignmentImplementation : IAssignment
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
        throw new NotImplementedException();
    }

    public void DeleteAll()
    {
        throw new NotImplementedException();
    }

    public Assignment? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Assignment> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Assignment item)
    {
        throw new NotImplementedException();
    }
}

