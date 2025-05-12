namespace Dal;
using DO;
using DalApi;

/// <summary>
/// Implementation of the ICall interface for managing Call entities in the data layer.
/// </summary>
internal class CallImplementation : ICall
{
    /// <summary>
    /// Creates a new Call entity in the data source.
    /// </summary>
    /// <param name="item">The Call object to be created.</param>
    /// <returns>The unique ID of the newly created Call.</returns>
    /// <exception cref="DalAlreadyExistsException">Thrown if a Call with the same ID already exists.</exception>
    public int Create(Call item)
    {
        Call c = DataSource.Calls.Find(element => element.Id == item.Id);
        if (c != null)
            throw new DalAlreadyExistsException($"An object of type Call with this id {item.Id} already exists");
        int newId = Dal.Config.NextCallId;
        Call callCopy = new Call(newId, item.TypeOfCall, item.Description, item.Address, item.latitude, item.longitude
            , item.riskRange, item.OpeningTime, item.Status, item.MaxClosingTime);
        DataSource.Calls.Add(callCopy);
        return callCopy.Id;
    }

    /// <summary>
    /// Deletes a Call entity from the data source by its ID.
    /// </summary>
    /// <param name="id">The unique ID of the Call to be deleted.</param>
    /// <exception cref="DalDoesNotExistException">Thrown if a Call with the specified ID does not exist.</exception>
    public void Delete(int id)
    {
        Call c = DataSource.Calls.Find(element => element.Id == id);
        if (c == null)
            throw new DalDoesNotExistException($"An object of type Call with this Id does not exist");
        DataSource.Calls.Remove(c);
    }

    /// <summary>
    /// Deletes all Call entities from the data source.
    /// </summary>
    public void DeleteAll()
    {
        DataSource.Calls.Clear();
    }

    /// <summary>
    /// Reads a Call entity from the data source by its ID.
    /// </summary>
    /// <param name="id">The unique ID of the Call to be read.</param>
    /// <returns>The Call object if found, otherwise null.</returns>
    public Call? Read(int id)
    {
        Call c = DataSource.Calls.FirstOrDefault(element => element.Id == id)!;
        if (c != null)
            return c;
        return null;
    }

    /// <summary>
    /// Reads all Call entities from the data source, optionally filtered by a predicate.
    /// </summary>
    /// <param name="filter">A predicate to filter the Calls. If null, all Calls are returned.</param>
    /// <returns>An IEnumerable of Call objects that match the filter.</returns>
    public IEnumerable<Call> ReadAll(Func<Call, bool>? filter = null) //stage 2
       => filter == null
           ? DataSource.Calls.Select(item => item)
           : DataSource.Calls.Where(filter);

    /// <summary>
    /// Updates an existing Call entity in the data source.
    /// </summary>
    /// <param name="item">The updated Call object.</param>
    /// <exception cref="DalDoesNotExistException">Thrown if a Call with the specified ID does not exist.</exception>
    public void Update(Call item)
    {
        Call c = DataSource.Calls.Find(element => element.Id == item.Id);
        if (c == null)
            throw new DalDoesNotExistException("An object of type Volunteer with this Id does not exist");
        DataSource.Calls.Remove(c);
        DataSource.Calls.Add(item);
    }

    /// <summary>
    /// Reads a Call entity from the data source using a filter function.
    /// </summary>
    /// <param name="filter">A predicate to filter the Calls.</param>
    /// <returns>The first Call object that matches the filter, or null if no match is found.</returns>
    /// <exception cref="NullException">Thrown if the filter function is null.</exception>
    public Call? Read(Func<Call, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");

        return DataSource.Calls.Cast<Call>().FirstOrDefault(filter);
    }
}
