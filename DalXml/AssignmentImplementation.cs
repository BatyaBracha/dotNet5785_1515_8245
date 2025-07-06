namespace Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

/// <summary>
/// Implements assignment data access logic for the DAL XML layer.
/// </summary>
internal class AssignmentImplementation : IAssignment
{
    /// <summary>
    /// Creates a new assignment and adds it to the data storage.
    /// </summary>
    /// <param name="item">The assignment to be created.</param>
    /// <returns>The ID of the newly created assignment.</returns>
        /// <summary>
    /// Creates a new assignment and adds it to the XML data source.
    /// </summary>
    /// <param name="item">The assignment to create.</param>
    /// <returns>The ID of the created assignment.</returns>
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
        /// <summary>
    /// Reads an assignment by ID from the XML data source.
    /// </summary>
    /// <param name="id">The ID of the assignment to read.</param>
    /// <returns>The assignment if found; otherwise, null.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Assignment? Read(int id)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        Assignment a = Assignments.FirstOrDefault(item => item.Id == id)!;
        if (a != null)
            return a;
        return null;
    }
        /// <summary>
    /// Reads an assignment matching a filter from the XML data source.
    /// </summary>
    /// <param name="filter">Predicate function to match assignments.</param>
    /// <returns>The first matching assignment, or null if none found.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Assignment? Read(Func<Assignment, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        return Assignments.FirstOrDefault(filter);
    }
        /// <summary>
    /// Reads all assignments, optionally filtered by a predicate.
    /// </summary>
    /// <param name="filter">Optional predicate function to filter assignments.</param>
    /// <returns>Enumerable of assignments.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null)
    {//stage 2
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);

        if (filter == null)
            return Assignments.Select(item => item);
        else
            return Assignments.Where(filter);
    }
        /// <summary>
    /// Updates an existing assignment in the XML data source.
    /// </summary>
    /// <param name="item">The assignment with updated information.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Assignment item)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Assignments.RemoveAll(it => it.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"Assignment with ID={item.Id} does Not exist");
        Assignments.Add(item);
        XMLTools.SaveListToXMLSerializer(Assignments, Config.s_assignments_xml);

    }
        /// <summary>
    /// Deletes an assignment from the XML data source by ID.
    /// </summary>
    /// <param name="id">The ID of the assignment to delete.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Assignments.RemoveAll(it => it.Id == id) == 0)
            throw new DalDoesNotExistException($"Assignment with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(Assignments, Config.s_assignments_xml);
    }
        /// <summary>
    /// Deletes all assignments from the XML data source.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteAll()
    {
        XMLTools.SaveListToXMLSerializer(new List<Assignment>(), Config.s_assignments_xml);
    }

}