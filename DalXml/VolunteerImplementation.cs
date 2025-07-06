namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

/// <summary>
/// Implements volunteer data access logic for the DAL XML layer.
/// </summary>
internal class VolunteerImplementation : IVolunteer
{
    /// <summary>
    /// Converts an XElement to a Volunteer object.
    /// </summary>
    /// <param name="v">The XElement to convert.</param>
    /// <returns>A Volunteer object.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
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
        /// <summary>
    /// Creates a new volunteer and adds it to the XML data source.
    /// </summary>
    /// <param name="item">The volunteer to create.</param>
    /// <returns>The ID of the created volunteer.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
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
            new XElement("Active", item.Active),
            new XElement("MaxDistance", item.MaxDistance),
            new XElement("TypeOfDistance", item.TypeOfDistance.ToString() ?? "")

        );

        volunteersRoot.Add(newVolunteer);
        XMLTools.SaveListToXMLElement(volunteersRoot, Config.s_volunteers_xml);
        return item.Id;
    }
        /// <summary>
    /// Deletes a volunteer from the XML data source by ID.
    /// </summary>
    /// <param name="id">The ID of the volunteer to delete.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
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
        /// <summary>
    /// Deletes all volunteers from the XML data source.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteAll()
    {
        XElement emptyRoot = new XElement("ArrayOfVolunteer");
        XMLTools.SaveListToXMLElement(emptyRoot, Config.s_volunteers_xml);
    }
        /// <summary>
    /// Reads a volunteer by ID from the XML data source.
    /// </summary>
    /// <param name="id">The ID of the volunteer to read.</param>
    /// <returns>The volunteer if found; otherwise, null.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Volunteer? Read(int id)
    {
        XElement? volunteerElm = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml)
            .Elements()
            .FirstOrDefault(st => (int?)st
            .Element("Id") == id);
        return volunteerElm is null ? null : GetVolunteer(volunteerElm);
    }
        /// <summary>
    /// Reads a volunteer matching a filter from the XML data source.
    /// </summary>
    /// <param name="filter">Predicate function to match volunteers.</param>
    /// <returns>The first matching volunteer, or null if none found.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Volunteer? Read(Func<Volunteer, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml)
            .Elements()
            .Select(s => GetVolunteer(s))
            .FirstOrDefault(filter);
    }
        /// <summary>
    /// Reads all volunteers, optionally filtered by a predicate.
    /// </summary>
    /// <param name="filter">Optional predicate function to filter volunteers.</param>
    /// <returns>Enumerable of volunteers.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool>? filter = null)
    {
        IEnumerable<Volunteer> Volunteers = XMLTools
            .LoadListFromXMLElement(Config.s_volunteers_xml)
            .Elements()
            .Select(v => GetVolunteer(v));
        return filter == null ? Volunteers : Volunteers.Where(filter);
    }
        /// <summary>
    /// Updates an existing volunteer in the XML data source.
    /// </summary>
    /// <param name="item">The volunteer with updated information.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
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
            new XElement("Active", item.Active),
            new XElement("MaxDistance", item.MaxDistance),
            new XElement("TypeOfDistance", item.TypeOfDistance.ToString() ?? "")
        ));

        // שומר את השינויים בקובץ
        XMLTools.SaveListToXMLElement(volunteersRootElem, Config.s_volunteers_xml);
    }


}
