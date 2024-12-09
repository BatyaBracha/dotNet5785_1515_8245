namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

internal class VolunteerImplementation : IVolunteer
{
    static Volunteer getVolunteer(XElement v)
    {
        //string r = (string?)v.Element("Role") ?? ""; // Your string value
        //Role? Role = r.ToEnumNullable<Role>();
        return new DO.Volunteer()
        {
            Id = v.ToIntNullable("Id") ?? throw new FormatException("can't convert id"),
            Name = (string?)v.Element("Name") ?? "",
            Phone = (string?)v.Element("Phone") ?? "",
            Email = (string?)v.Element("Email") ?? "",
            Password = (string?)v.Element("Password") ?? "",
            Address = (string?)v.Element("Address") ?? "",
            latitude = v.ToDoubleNullable("latitude") ?? throw new FormatException("can't convert latitude"),
            longitude = v.ToDoubleNullable("longitude") ?? throw new FormatException("can't convert longitude"),
            Role = (string?)v.Element("Role") ?? "".ToEnumNullable<Role>(),
            Active = (bool?)v.Element("IsActive") ?? false
            //MaxDistance,
            //TypeOfDistance
        };
    }

    public int Create(Volunteer item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteAll()
    {
        throw new NotImplementedException();
    }

    public Volunteer? Read(int id)
    {
        XElement? volunteerElm =
    XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml).Elements().FirstOrDefault(st => (int?)st.Element("Id") == id);
        return volunteerElm is null ? null : getStudent(volunteerElm);
    }

    public Volunteer? Read(Func<Volunteer, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml).Elements().Select(s => getStudent(s)).FirstOrDefault(filter);
    }

    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Volunteer item)
    {
        XElement volunteersRootElem = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);

        (volunteersRootElem.Elements().FirstOrDefault(st => (int?)st.Element("Id") == item.Id)
        ?? throw new DO.DalDoesNotExistException($"Volunteer with ID={item.Id} does Not exist"))
                .Remove();

        volunteersRootElem.Add(new XElement("Volunteer", createVolunteerElement(item)));

        XMLTools.SaveListToXMLElement(volunteersRootElem, Config.s_volunteers_xml);
    }

}
