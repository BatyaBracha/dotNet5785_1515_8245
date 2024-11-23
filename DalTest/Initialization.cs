namespace DalTest;
using DalApi;
using DO;
using System;

public static class Initialization
{
    private static IVolunteer? s_dalVolunteer; //stage 1
    private static ICall? s_dalCall; //stage 1
    private static IAssignment? s_dalAssignment; //stage 1
    private static IConfig? s_dalConfig; //stage 1
    private static readonly Random s_rand = new();
    private static void createVolunteers()
    {
        string[] volunteerNames =
            { "Dani Levy", "Eli Amar", "Yair Cohen", "Ariela Levin", "Dina Klein", "Shira Israelof" };

        string phoneStarter = "054554110";
        string emailEnder = "@organization.org.il";
        string startedPassword =  "1234567";
        int i = 0;
        foreach (var name in volunteerNames)
        {
            int id;
            do
                id = s_rand.Next(400000000, 200000000);
            while (s_dalVolunteer!.Read(id) != null);
            string phone = $"{phoneStarter}{i++}";
            string email = $"{name}{emailEnder}";

            string password=$"{startedPassword}{i}";
            string address = Addresses[i];

            double latitude = 31.7769;

            double longitude = 35.2300;

            Role role = (Role)s_rand.Next(0, 2);

            bool active = s_rand.Next(0, 2) == 1;

            double MaxDistance = i * 100;

            TypeOfDistance TypeOfDistance= (TypeOfDistance)s_rand.Next(0, 2);

            s_dalVolunteer!.Create(new(id, name, phone, email, password, address, latitude, longitude, role, Active, MaxDistance, TypeOfDistance));
        }
    }
    private static void createAssignments()
    {
        int[] idArr = new int[5];
        for (int j = 0; j < idArr.Length; j++)
        {
            idArr[j] =  s_rand.Next(400000000, 200000000);
        }
        foreach (var id in idArr)
        {
            int callId;
            do
                callId = s_rand.Next(400000000, 200000000);
            while (s_dalVolunteer!.Read(callId) != null);
            int volunteerId;
            do
                volunteerId = s_rand.Next(400000000, 200000000);
            while (s_dalVolunteer!.Read(volunteerId) != null);

            DateTime treatmentdStartTime = startDate();
            DateTime treatmentEndTime = endTime();
            TypeOfTreatmentEnding typeOfTreatmentEnding= (TypeOfTreatmentEnding)s_rand.Next(0, 2);
            s_dalAssignment!.Create(new(id, callId, volunteerId, treatmentdStartTime, treatmentEndTime, typeOfTreatmentEnding);
        }
    }

    private static void createCalls()
    {
        int[] idArr = new int[5];
        for (int j = 0; j < idArr.Length; j++)
        {
            idArr[j] = s_rand.Next(400000000, 200000000);
        }
        foreach (var id in idArr)
        {
            TypeOfCall typeOfCall = (TypeOfCall)s_rand.Next(0, 2);
            string description = "";
            string address = Addresses[idArr[id]];
            double latitude = 31.7769;
            double longitude = 35.2300;
            DateTime OpeningTime=startDate();
            DateTime maxClosingTime=endTime();
            s_dalCall!.Create(new(id, typeOfCall, description, address, latitude, longitude, OpeningTime, maxClosingTime));
        }
    }
    private static DateTime startDate()
    {
        DateTime start = new DateTime(s_dalConfig.Clock.Year - 2, 1, 1); //stage 1
        int range = (s_dalConfig.Clock - start).Days; //stage 1
        return start.AddDays(s_rand.Next(range));
    }
    private static DateTime endTime()
    {
        DateTime end = new DateTime(s_dalConfig.Clock.Year + 2, 1, 1); //stage 1
        int range = (s_dalConfig.Clock - end).Days; //stage 1
        return end.AddDays(s_rand.Next(range));
    }
    public static void Do(IAssignment? dalAssignment, ICall? dalcall, IConfig? dalConfig, IVolunteer? dalVolunteer)
    {
        //s_dalConfig = dalConfig ?? throw new NullReferenceException("DAL can not be null!");
        s_dalAssignment = dalAssignment ?? throw new NullReferenceException("DAL can not be null!");
        Console.WriteLine("Reset Configuration values and List values...");
        s_dalAssignment.Reset(); //stage 1
        s_dalAssignment.DeleteAll(); //stage 1
        Console.WriteLine("Initializing Students list ...");
        createAssignments();

        s_dalCall = dalcall ?? throw new NullReferenceException("DAL can not be null!");
        Console.WriteLine("Reset Configuration values and List values...");
        s_dalCall.Reset(); //stage 1
        s_dalCall.DeleteAll(); //stage 1
        Console.WriteLine("Initializing Students list ...");
        createCalls();

        s_dalVolunteer = dalVolunteer ?? throw new NullReferenceException("DAL can not be null!");
        Console.WriteLine("Reset Configuration values and List values...");
        s_dalVolunteer.Reset(); //stage 1
        s_dalVolunteer.DeleteAll(); //stage 1
        Console.WriteLine("Initializing Students list ...");
        createVolunteers();
    }
