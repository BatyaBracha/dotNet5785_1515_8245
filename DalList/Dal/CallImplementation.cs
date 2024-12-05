

namespace Dal;
using DO;
using DalApi;

internal class CallImplementation : ICall
{
    public int Create(Call item)
    {
        Call c = DataSource.Calls.Find(element => element.Id == item.Id);
        if (c != null)
            throw new DalAlreadyExistsException($"An object of type Call with this id {item.Id} already exists");
        int newId = Dal.Config.NextCallId;
        Call callCopy = new Call(newId,item.TypeOfCall,item.Description,item.Address,item.latitude,item.longitude,item.riskRange,item.OpeningTime,item.MaxClosingTime);
        DataSource.Calls.Add(callCopy);
        return callCopy.Id;
    }
   
    public void Delete(int id)
    {
        Call c = DataSource.Calls.Find(element => element.Id == id);
        if (c == null)
            throw new DalDoesNotExistException($"An object of type Call with this Id does not exist");
        DataSource.Calls.Remove(c);
    }

    public void DeleteAll()
    {
        DataSource.Calls.Clear();  
    }

    public Call? Read(int id)
    {
        Call c = DataSource.Calls.FirstOrDefault(element => element.Id == id)!;
        if (c != null)
            return c;
        return null;
    }

    public IEnumerable<Call> ReadAll(Func<Call, bool>? filter = null) //stage 2
       => filter == null
           ? DataSource.Calls.Select(item => item)
           : DataSource.Calls.Where(filter);

    public void Update(Call item)
    {
        Call c = DataSource.Calls.Find(element => element.Id == item.Id);
        if (c == null)
            throw new DalDoesNotExistException("An object of type Volunteer with this Id does not exist");
        DataSource.Calls.Remove(c);
        DataSource.Calls.Add(item);
    }

    public Call? Read(Func<Call, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");

        return DataSource.Calls.Cast<Call>().FirstOrDefault(filter);
    }

}
