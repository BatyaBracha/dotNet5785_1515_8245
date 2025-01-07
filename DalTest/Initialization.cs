
namespace DalTest;
using DalApi;
using DO;



public static class Initialization
{
    private static IDal? s_dal;
    private static readonly Random s_rand = new();
    private static void CreateVolunteer()
    {
        const int MIN_ID = 100000000;
        const int MAX_ID = 999999999;
        string[] names = { "Tzipy", "Chani", "Yehudit", "Shira", "Moishy" , "Efraim", "Shloimy", "Yosi", "Yoni", "Avi", "Chaim", "Ester", "Rachel", "Shani", "Michali" };
        string[] phones = { "0527614421", "0527614422", "0527614423", "0527614424", "0527614425", "0527614426", "0527614427", "0527614428", "0527614429", "0527614430", "0527614431", "0527614432", "0527614433", "0527614434", "0527614435" };
        string[] addresses = { "Malchei Israel 12", "Minchat Itzchak 23", "Hashofar 3", "Hare'em 14", "Eben Ezra 6", "HaTzvi 4", "Hakfir 5", "Hadishon 6", "Ha'ari 13", "Hatan 17", "Tchelet Mordechei 18", "Hamarkalim 4", "Rabi Akiva 15", "Hazon Ish 53", "Harav Shach 3" };
        (double Latitude, double Longitude)[] coordinates = {(31.771959, 35.217018 ), (  31.775469, 35.229568 ),  (  31.774578, 35.207031 ),
  (  31.780125,  35.207889 ),  (  31.792178,  35.205321 ), (  31.784222,  35.213127 ), (  31.797433,  35.214278 ), (  31.798189,  35.206539 ),
   (  31.794056,  35.221325 ),(  31.798832,  35.214994 ),(  31.799845,  35.227146 ), (  31.780829,  35.229744 ),
  (31.781869,  35.236349 ),(  31.799467,  35.240458 ), (  31.788469,  35.235679 )};
        string[] passwords = { "0ZRhtg@fX8", "Yyxk%zHBBv", "xaCVl@@YVk", "op*pix&iZ&", "(zdMnv9K26", "A7bR#ZX^^2", "pnK6U31HCR", "2tvFx(*92!", "C0pjk1NzwI", "aAmrbepq#3", "%1IlDaLWn%", "chNF08*vWn", "qBf$@2HDyS", "ca*)&UrEgJ", "p!L2z#7h@m}" };
        int[] maxDistances = { 219, 748, 298, 767, 893, 419, 974, 908, 959, 450, 166, 978, 595, 250, 575 };
        s_dal!.Volunteer.Create(new(s_rand.Next(MIN_ID, MAX_ID), "Mali Horowitz", "0583278625", "mh0583278625@gmail.com", "password", "Ha'ayalot 25", 31.8, 35.1, Role.ADMINISTRATOR, true, 250, (TypeOfDistance)s_rand.Next(0, 2)));

        for (int i = 0; i < 15; i++)
        {
            s_dal!.Volunteer.Create(new(s_rand.Next(MIN_ID, MAX_ID), names[i], phones[i], $"{names[i]}{phones[i]}@gmail.com", passwords[i], addresses[i], coordinates[i].Latitude, coordinates[i].Longitude, Role.STANDARD, true, maxDistances[i], (TypeOfDistance)s_rand.Next(0, 2)));
        }

    }
    private static void CreateCall()
    {
        string[] addresses = { "Herzl St, Tel Aviv", "Jaffa St, Jerusalem", "Ben Gurion Blvd, Haifa", "Main St, Beersheba", "Eilat Promenade, Eilat", "Rothschild Blvd, Tel Aviv", "Dizengoff St, Tel Aviv", "Ben Yehuda St, Tel Aviv", "Shderot Ben Gurion, Netanya", "David HaMelech St, Herzliya" };
        (double Latitude, double Longitude)[] coordinates =
        { (32.0853, 34.7818), (31.7683, 35.2137), (32.7940, 34.9896), (31.2518, 34.7913), (29.5581, 34.9482), (32.0656, 34.7770), (32.0755, 34.7756), (32.0838, 34.7698), (32.3320, 34.8599), (32.1673, 34.8360) };
        string[] descriptions =
        {
        "Flat tire on the highway.",
        "Car battery is dead, needs jump start.",
        "Ran out of fuel near the city center.",
        "Locked keys inside the vehicle.",
        "Vehicle recovery required from the beach.",
        "Minor mechanical issue near the mall.",
        "Medical emergency near the park.",
        "Car stuck in the sand, needs assistance.",
        "Flat tire on the road to Jerusalem.",
        "Jump start required at the parking lot."
    };

        // בדיקה ששעון המערכת מאותחל
        if (s_dal.Config.Clock == default)
            throw new Exception("Clock value is not initialized in configuration.");

        DateTime start = new DateTime(
            s_dal.Config.Clock.Year,
            s_dal.Config.Clock.Month,
            s_dal.Config.Clock.Day,
            s_dal.Config.Clock.Hour - 7, 0, 0);

        int range = (int)(s_dal.Config.Clock - start).TotalMinutes;
        if (range <= 0)
            throw new Exception("Invalid time range for creating calls.");

        for (int i = 0; i < 50; i++)
        {
            int startingTime = s_rand.Next(range);
            int index = s_rand.Next(addresses.Length);
            Call newCall = new Call(
                0,
                (TypeOfCall)s_rand.Next(Enum.GetValues(typeof(TypeOfCall)).Length),
                descriptions[s_rand.Next(descriptions.Length)],
                addresses[index],
                coordinates[index].Latitude,
                coordinates[index].Longitude,
                s_dal.Config.RiskRange,
                start.AddMinutes(startingTime),
                start.AddMinutes(startingTime + s_rand.Next(30, 360))
            );

            // יצירה ב-DAL
            s_dal.Call.Create(newCall);
        }
    }

    private static void CreateAssignment()
    {
        List<Call>? callsList = s_dal!.Call!.ReadAll().ToList();
        List<Volunteer>? volunteersList = s_dal.Volunteer!.ReadAll().ToList();

        for (int i = 0; i < 50; i++)
        {
            // חישוב הזמן ההתחלתי והזמן הסופי עבור כל קריאה
            DateTime minTime = callsList[i].OpeningTime; // הזמן המינימלי
            DateTime maxTime = (DateTime)callsList[i].MaxClosingTime!; // הזמן המקסימלי
            //TimeSpan diff = maxTime - minTime - TimeSpan.FromHours(2);
            TimeSpan diff = maxTime - minTime - TimeSpan.FromHours(2);
            if (diff.TotalMinutes <= 0)
            {
                diff = TimeSpan.FromMinutes(1); // ערך ברירת מחדל למניעת שגיאה
            }
            DateTime randomTime = minTime.AddMinutes(s_rand.Next((int)diff.TotalMinutes));
            s_dal!.Assignment.Create(new Assignment(0,callsList[i].Id, volunteersList[s_rand.Next(volunteersList.Count())].Id, randomTime, randomTime.AddHours(2), (TypeOfTreatmentEnding)s_rand.Next(Enum.GetValues(typeof(TypeOfTreatmentEnding)).Length - 1)));
        }
    }
   
    public static void Do() //stage 1
    {
        //s_dal = dal ?? throw new NullException("DAL object can not be null!");
        s_dal = DalApi.Factory.Get; //stage 4


        Console.WriteLine("Reset Configuration values and List values...");
        s_dal.ResetDB();

        Console.WriteLine("Initializing Volunteers list ...");
        Console.WriteLine("Initializing Calls list ...");
        Console.WriteLine("Initializing Assignments list ...");

        CreateVolunteer();
        CreateCall();
        CreateAssignment();
    }
}
