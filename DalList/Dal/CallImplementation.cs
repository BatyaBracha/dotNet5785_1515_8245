

namespace Dal;
using DO;
using DalApi;

public class CallImplementation : 
{
    public int Create(Call item)
    {
        Call c = DataSource.Calls.Find(element => element.Id == item.Id);
        if (c != null)
            throw new Exception($"An object of type Call with this id {item.Id} already exists");
        int newId = Dal.Config.NextCallId;
        Call callCopy = new Call(newId,item.TypeOfCall,item.Description,item.Address,item.latitude,item.longitude,item.riskRange,item.OpeningTime,item.MaxClosingTime);
        DataSource.Calls.Add(callCopy);
        return callCopy.Id;
    }
   
    public void Delete(int id)
    {
        Call c = DataSource.Calls.Find(element => element.Id == id);
        if (c == null)
            throw new Exception($"An object of type Call with this Id does not exist");
        DataSource.Calls.Remove(c);
    }

    public void DeleteAll()
    {
        DataSource.Calls.Clear();  
    }

    public Call? Read(int id)
    {
        Call c = DataSource.Calls.Find(element => element.Id == id);
        if (c != null)
            return c;
        return null;
    }

    public List<Call> ReadAll()
    {
        return new List<Call>(DataSource.Calls);
    }

    public void Update(Call item)
    {
        Call c = DataSource.Calls.Find(element => element.Id == item.Id);
        if (c == null)
            throw new Exception("An object of type Volunteer with this Id does not exist");
        DataSource.Calls.Remove(c);
        DataSource.Calls.Add(item);
    }

}
