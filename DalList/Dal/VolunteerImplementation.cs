

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

internal class VolunteerImplementation :IVolunteer
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  int Create(Volunteer item)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == item.Id);
        if (v != null)
            throw new DalAlreadyExistsException("An object of type volunteer with this id already exists");
        DataSource.Volunteers.Add(item);
        return item.Id;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == id);
        if (v == null)
            throw new DalDoesNotExistException("An object of type Volunteer with this Id does not exist");
        DataSource.Volunteers.Remove(v);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteAll()
    {
        DataSource.Volunteers.Clear();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Volunteer? Read(int id)
    {
        Volunteer v = DataSource.Volunteers.FirstOrDefault(element => element.Id == id)!;
        if (v != null)
            return v;
        return null;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool>? filter = null) //stage 2
      => filter == null
          ? DataSource.Volunteers.Select(item => item)
          : DataSource.Volunteers.Where(filter);
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Volunteer item)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == item.Id);
        if (v == null)
            throw new DalDoesNotExistException("An object of type Volunteer with this Id does not exist");
        DataSource.Volunteers.Remove(v);
        DataSource.Volunteers.Add(item);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Volunteer? Read(Func<Volunteer, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");

        return DataSource.Volunteers.Cast<Volunteer>().FirstOrDefault(filter);
    }
}
