

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

internal class VolunteerImplementation :IVolunteer
{
    public  int Create(Volunteer item)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == item.Id);
        if (v != null)
            throw new DalAlreadyExistsException("An object of type volunteer with this id already exists");
        DataSource.Volunteers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == id);
        if (v == null)
            throw new DalDoesNotExistException("An object of type Volunteer with this Id does not exist");
        DataSource.Volunteers.Remove(v);
    }

    public void DeleteAll()
    {
        DataSource.Volunteers.Clear();
    }

    public Volunteer? Read(int id)
    {
        Volunteer v = DataSource.Volunteers.FirstOrDefault(element => element.Id == id)!;
        if (v != null)
            return v;
        return null;
    }

    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool>? filter = null) //stage 2
      => filter == null
          ? DataSource.Volunteers.Select(item => item)
          : DataSource.Volunteers.Where(filter);

    public void Update(Volunteer item)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == item.Id);
        if (v == null)
            throw new DalDoesNotExistException("An object of type Volunteer with this Id does not exist");
        DataSource.Volunteers.Remove(v);
        DataSource.Volunteers.Add(item);
    }

    public Volunteer? Read(Func<Volunteer, bool> filter)
    {
        if (filter == null)
            throw new NullException($"{nameof(filter)} Filter function cannot be null");

        return DataSource.Volunteers.Cast<Volunteer>().FirstOrDefault(filter);
    }
}
