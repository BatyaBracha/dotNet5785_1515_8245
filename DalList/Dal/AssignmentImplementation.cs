

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
/// Implements assignment-related data access operations for DalList.
/// </summary>
internal class AssignmentImplementation : IAssignment
{
    /// <summary>
    /// Creates a new assignment and adds it to the data source.
    /// </summary>
    /// <param name="item">The assignment to create.</param>
    /// <returns>The ID of the newly created assignment.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(Assignment item)
    {
        int newId = Dal.Config.NextAssignmentId;
        Assignment copy = new Assignment(newId, item.CallId, item.VolunteerId, item.TreatmentStartTime, item.TreatmentEndTime, item.TypeOfTreatmentEnding, item.AssignmentStatus);
        DataSource.Assignments.Add(copy);
        return newId;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        Assignment a = DataSource.Assignments.Find(element => element.Id == id);
        if (a == null)
            throw new DalDoesNotExistException($"An object of type Volunteer with this Id {id} does not exist");
        DataSource.Assignments.Remove(a);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteAll()
    {
        DataSource.Assignments.Clear();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Assignment? Read(int id)
    {
        Assignment a = DataSource.Assignments.FirstOrDefault(item => item.Id == id)!;
        if (a != null)
            return a;
        return null;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null) 
        => filter == null
            ? DataSource.Assignments.Select(item => item)
            : DataSource.Assignments.Where(filter);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Assignment item)
    {
        Assignment a = DataSource.Assignments.Find(element => element.Id == item.Id);
        if (a == null)
            throw new DalDoesNotExistException($"An object of type Volunteer with this Id {item.Id} does not exist");
        DataSource.Assignments.Remove(a);
        DataSource.Assignments.Add(item);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Assignment? Read(Func<Assignment, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");

        return DataSource.Assignments.Cast<Assignment>().FirstOrDefault(filter);
    }

}

