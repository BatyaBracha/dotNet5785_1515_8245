namespace Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

/// <summary>
/// Implements call data access logic for the DAL XML layer.
/// </summary>
internal class CallImplementation : ICall
{
    /// <summary>
    /// Creates a new call and adds it to the data storage.
    /// </summary>
    /// <param name="item">The call to be created.</param>
    /// <returns>The ID of the newly created call.</returns>
        /// <summary>
    /// Creates a new call and adds it to the XML data source.
    /// </summary>
    /// <param name="item">The call to create.</param>
    /// <returns>The ID of the created call.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(Call item)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        Call c= Calls.Find(element => element.Id == item.Id)!;
        if (c != null)
            throw new DalAlreadyExistsException($"An object of type Call with this id {item.Id} already exists");
        int newId = Config.NextCallId;
        Call callCopy = new Call(newId, item.TypeOfCall, item.Description, item.Address, item.latitude, item.longitude, item.riskRange, item.OpeningTime,item.Status, item.MaxClosingTime);
        Calls.Add(callCopy);
        XMLTools.SaveListToXMLSerializer(Calls, Config.s_calls_xml);
        return callCopy.Id;
    }
        /// <summary>
    /// Reads a call by ID from the XML data source.
    /// </summary>
    /// <param name="id">The ID of the call to read.</param>
    /// <returns>The call if found; otherwise, null.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Call? Read(int id)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        Call c = Calls.FirstOrDefault(element => element.Id == id)!;
        if (c != null)
            return c;
        return null;
    }
        /// <summary>
    /// Reads a call matching a filter from the XML data source.
    /// </summary>
    /// <param name="filter">Predicate function to match calls.</param>
    /// <returns>The first matching call, or null if none found.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Call? Read(Func<Call, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        return Calls.Cast<Call>().FirstOrDefault(filter);
    }
        /// <summary>
    /// Reads all calls, optionally filtered by a predicate.
    /// </summary>
    /// <param name="filter">Optional predicate function to filter calls.</param>
    /// <returns>Enumerable of calls.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Call> ReadAll(Func<Call, bool>? filter = null)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (filter == null)
            return Calls.Select(item => item);
        else
            return Calls.Where(filter);
    }
        /// <summary>
    /// Updates an existing call in the XML data source.
    /// </summary>
    /// <param name="item">The call with updated information.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Call item)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (Calls.RemoveAll(it => it.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"Call with ID={item.Id} does Not exist");
        Calls.Add(item);
        XMLTools.SaveListToXMLSerializer(Calls, Config.s_calls_xml);
    }
        /// <summary>
    /// Deletes a call from the XML data source by ID.
    /// </summary>
    /// <param name="id">The ID of the call to delete.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (Calls.RemoveAll(it => it.Id == id) == 0)
            throw new DalDoesNotExistException($"Call with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(Calls, Config.s_calls_xml);
    }
        /// <summary>
    /// Deletes all calls from the XML data source.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteAll()
    {
        XMLTools.SaveListToXMLSerializer(new List<Call>(), Config.s_calls_xml);
    }

}
