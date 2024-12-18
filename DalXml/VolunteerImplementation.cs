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
    static Volunteer GetVolunteer(XElement v)
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
            latitude = v.ToDoubleNullable("latitude") ??0.0,
            //throw new FormatException("can't convert latitude"),
            longitude = v.ToDoubleNullable("longitude") ?? 0.0,
            //throw new FormatException("can't convert longitude"),
            Role = v.ToEnumNullable<Role>("Role") ?? throw new FormatException("can't convert Role"),
            Active = (bool?)v.Element("Active") ?? false,
            MaxDistance = v.ToDoubleNullable("MaxDistance") ?? throw new FormatException("can't convert MaxDistance"),
            TypeOfDistance = v.ToEnumNullable<TypeOfDistance>("TypeOfDistance") ?? throw new FormatException("can't convert TypeOfDistance")
        };
    }

    public int Create(Volunteer item)//AI
    {
        XElement volunteersRoot = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);

        if (volunteersRoot.Elements().Any(v => (int?)v.Element("Id") == item.Id))
        {
            throw new InvalidOperationException($"A volunteer with ID {item.Id} already exists.");
        }

        XElement newVolunteer = new XElement("Volunteer",
            new XElement("Id", item.Id),
            new XElement("Name", item.Name),
            new XElement("Phone", item.Phone),
            new XElement("Email", item.Email),
            new XElement("Password", item.Password),
            new XElement("Address", item.Address),
            new XElement("latitude", item.latitude),
            new XElement("longitude", item.longitude),
            new XElement("Role", item.Role.ToString() ?? ""),
            new XElement("IsActive", item.Active),
            new XElement("MaxDistance", item.MaxDistance),
            new XElement("TypeOfDistance", item.TypeOfDistance.ToString() ?? "")

        );

        volunteersRoot.Add(newVolunteer);
        XMLTools.SaveListToXMLElement(volunteersRoot, Config.s_volunteers_xml);
        return item.Id;
    }

    public void Delete(int id)
    {
        XElement volunteersRootElem = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);
        XElement volunteerToDelete = volunteersRootElem
            .Elements()
            .FirstOrDefault(v => (int?)v.Element("Id") == id)
            ?? throw new DO.DalDoesNotExistException($"Volunteer with ID={id} does not exist");
        volunteerToDelete.Remove();
        XMLTools.SaveListToXMLElement(volunteersRootElem, Config.s_volunteers_xml);
    }

    public void DeleteAll()
    {
        XElement emptyRoot = new XElement("ArrayOfVolunteer");
        XMLTools.SaveListToXMLElement(emptyRoot, Config.s_volunteers_xml);
    }

    public Volunteer? Read(int id)
    {
        XElement? volunteerElm =
    XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml).Elements().FirstOrDefault(st => (int?)st.Element("Id") == id);
        return volunteerElm is null ? null : GetVolunteer(volunteerElm);
    }

    public Volunteer? Read(Func<Volunteer, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml).Elements().Select(s => GetVolunteer(s)).FirstOrDefault(filter);
    }

    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool>? filter = null)
    {
        IEnumerable<Volunteer> Volunteers = XMLTools
            .LoadListFromXMLElement(Config.s_volunteers_xml)
            .Elements()
            .Select(v => GetVolunteer(v));
        return filter == null ? Volunteers : Volunteers.Where(filter);
    }

    //public void Update(Volunteer item)
    //{
    //    XElement volunteersRootElem = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);

    //    (volunteersRootElem.Elements().FirstOrDefault(st => (int?)st.Element("Id") == item.Id)
    //    ?? throw new DO.DalDoesNotExistException($"Volunteer with ID={item.Id} does Not exist"))
    //            .Remove();

    //    volunteersRootElem.Add(new XElement("Volunteer", Create(item)));

    //    XMLTools.SaveListToXMLElement(volunteersRootElem, Config.s_volunteers_xml);
    //}

    public void Update(Volunteer item)
    {
        XElement volunteersRootElem = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);

        // מחפש את המתנדב לפי ה-ID ומוודא שהוא קיים
        XElement volunteerToUpdate = volunteersRootElem.Elements()
            .FirstOrDefault(st => (int?)st.Element("Id") == item.Id)
            ?? throw new DO.DalDoesNotExistException($"Volunteer with ID={item.Id} does Not exist");

        // מסיר את המתנדב הקיים
        volunteerToUpdate.Remove();

        // מוסיף את המתנדב המעודכן תחת אותו ID
        volunteersRootElem.Add(new XElement("Volunteer",
            new XElement("Id", item.Id),
            new XElement("Name", item.Name),
            new XElement("Phone", item.Phone),
            new XElement("Email", item.Email),
            new XElement("Password", item.Password),
            new XElement("Address", item.Address),
            new XElement("latitude", item.latitude),
            new XElement("longitude", item.longitude),
            new XElement("Role", item.Role.ToString() ?? ""),
            new XElement("IsActive", item.Active),
            new XElement("MaxDistance", item.MaxDistance),
            new XElement("TypeOfDistance", item.TypeOfDistance.ToString() ?? "")
        ));

        // שומר את השינויים בקובץ
        XMLTools.SaveListToXMLElement(volunteersRootElem, Config.s_volunteers_xml);
    }


}
