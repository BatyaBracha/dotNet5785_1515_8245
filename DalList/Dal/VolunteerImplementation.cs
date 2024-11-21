

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq.Expressions;

public class VolunteerImplementation : IVolunteer
{
    public int Create(Volunteer item)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == item.Id);
        if (v != null)
            throw new Exception("An object of type volunteer with this id already exists");
        DataSource.Volunteers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == id);
        if (v == null)
            throw new Exception("An object of type Volunteer with this Id does not exist");
        DataSource.Volunteers.Remove(v);
    }

    public void DeleteAll()
    {
        DataSource.Volunteers.Clear();
    }

    public Volunteer? Read(int id)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == id);
        if (v != null)
            return v;
        return null;
    }

    public List<Volunteer> ReadAll()
    {
        return new List<Volunteer>(DataSource.Volunteers);
    }

    public void Update(Volunteer item)
    {
        Volunteer v = DataSource.Volunteers.Find(element => element.Id == item.Id);
        if (v == null)
            throw new Exception("An object of type Volunteer with this Id does not exist");
        DataSource.Volunteers.Remove(v);
        DataSource.Volunteers.Add(item);
    }
}
