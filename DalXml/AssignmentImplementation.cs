namespace Dal;
using DalApi;
using DO;

internal class AssignmentImplementation : IAssignment
{
    public int Create(Assignment item)
    {
        int newId = Config.NextAssignmentId;
        Assignment copy = new Assignment(newId, item.CallId, item.VolunteerId, item.TreatmentStartTime, item.TreatmentEndTime, item.TypeOfTreatmentEnding);
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        Assignments.Add(copy);
        XMLTools.SaveListToXMLSerializer(Assignments, Config.s_assignments_xml);
        return newId;
    }

    public Assignment? Read(int id)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        Assignment a = Assignments.FirstOrDefault(item => item.Id == id)!;
        if (a != null)
            return a;
        return null;
    }

    public Assignment? Read(Func<Assignment, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        return Assignments.FirstOrDefault(filter);
    }

    //public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null)
    //{
    //    List<Assignments> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
    //    => filter == null
    //        ? Assignments.Select(item => item)
    //        : Assignments.Where(filter);
    //}

    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null)
    {//stage 2
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);

        if (filter == null)
            return Assignments.Select(item => item);
        else
            return Assignments.Where(filter);
    }

    public void Update(Assignment item)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Assignments.RemoveAll(it => it.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"Assignment with ID={item.Id} does Not exist");
        Assignments.Add(item);
        XMLTools.SaveListToXMLSerializer(Assignments, Config.s_assignments_xml);

    }
    public void Delete(int id)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Assignments.RemoveAll(it => it.Id == id) == 0)
            throw new DalDoesNotExistException($"Assignment with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(Assignments, Config.s_assignments_xml);
    }
    public void DeleteAll()
    {
        XMLTools.SaveListToXMLSerializer(new List<Assignment>(), Config.s_assignments_xml);
    }

    int ICrud<Assignment>.Create(Assignment item)
    {
        throw new NotImplementedException();
    }
}