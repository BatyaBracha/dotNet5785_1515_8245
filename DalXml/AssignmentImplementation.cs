namespace Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

internal class AssignmentImplementation : IAssignment
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(Assignment item)
    {
        int newId = Config.NextAssignmentId;
        Assignment copy = new Assignment(newId, item.CallId, item.VolunteerId, item.TreatmentStartTime, item.TreatmentEndTime, item.TypeOfTreatmentEnding, item.AssignmentStatus);
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        Assignments.Add(copy);
        XMLTools.SaveListToXMLSerializer(Assignments, Config.s_assignments_xml);
        return newId;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Assignment? Read(int id)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        Assignment a = Assignments.FirstOrDefault(item => item.Id == id)!;
        if (a != null)
            return a;
        return null;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Assignment? Read(Func<Assignment, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        return Assignments.FirstOrDefault(filter);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null)
    {//stage 2
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);

        if (filter == null)
            return Assignments.Select(item => item);
        else
            return Assignments.Where(filter);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Assignment item)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Assignments.RemoveAll(it => it.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"Assignment with ID={item.Id} does Not exist");
        Assignments.Add(item);
        XMLTools.SaveListToXMLSerializer(Assignments, Config.s_assignments_xml);

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Assignments.RemoveAll(it => it.Id == id) == 0)
            throw new DalDoesNotExistException($"Assignment with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(Assignments, Config.s_assignments_xml);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteAll()
    {
        XMLTools.SaveListToXMLSerializer(new List<Assignment>(), Config.s_assignments_xml);
    }

}