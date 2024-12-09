namespace Dal;
using DalApi;
using DO;

internal class CallImplementation : ICall
{
    public int Create(Call item)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        Call c= Calls.Find(element => element.Id == item.Id)!;
        if (c != null)
            throw new DalAlreadyExistsException($"An object of type Call with this id {item.Id} already exists");
        int newId = Config.NextCallId;
        Call callCopy = new Call(newId, item.TypeOfCall, item.Description, item.Address, item.latitude, item.longitude, item.riskRange, item.OpeningTime, item.MaxClosingTime);
        Calls.Add(callCopy);
        XMLTools.SaveListToXMLSerializer(Calls, Config.s_calls_xml);
        return callCopy.Id;
    }
    public Call? Read(int id)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        Call c = Calls.FirstOrDefault(element => element.Id == id)!;
        if (c != null)
            return c;
        return null;
    }
    public Call? Read(Func<Call, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        return Calls.Cast<Call>().FirstOrDefault(filter);
    }
    public IEnumerable<Call> ReadAll(Func<Call, bool>? filter = null)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (filter == null)
            return Calls.Select(item => item);
        else
            return Calls.Where(filter);
    }
    public void Update(Call item)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (Calls.RemoveAll(it => it.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"Call with ID={item.Id} does Not exist");
        Calls.Add(item);
        XMLTools.SaveListToXMLSerializer(Calls, Config.s_calls_xml);
    }
    public void Delete(int id)
    {
        List<Call> Calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (Calls.RemoveAll(it => it.Id == id) == 0)
            throw new DalDoesNotExistException($"Call with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(Calls, Config.s_calls_xml);
    }

    public void DeleteAll()
    {
        XMLTools.SaveListToXMLSerializer(new List<Call>(), Config.s_calls_xml);
    }

    int ICrud<Call>.Create(Call item)
    {
        throw new NotImplementedException();
    }
}
