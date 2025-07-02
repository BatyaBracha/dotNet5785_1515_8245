
//namespace DalTest;
//using DalApi;
//using DO;



//public static class Initialization
//{
//    private static IDal? s_dal;
//    private static readonly Random s_rand = new();
//    private static void CreateVolunteer()
//    {
//        int[] Ids = { 123456782, 204589326, 315869241, 428193657, 537921483, 648291375, 759183624, 860195372, 972843165, 183726495, 294817536, 305918742, 416293857, 527384961, 638475120 };
//        string[] names = { "Tzipy", "Chani", "Yehudit", "Shira", "Moishy", "Efraim", "Shloimy", "Yosi", "Yoni", "Avi", "Chaim", "Ester", "Rachel", "Shani", "Michali" };
//        string[] phones = { "0527614421", "0527614422", "0527614423", "0527614424", "0527614425", "0527614426", "0527614427", "0527614428", "0527614429", "0527614430", "0527614431", "0527614432", "0527614433", "0527614434", "0527614435" };
//        string[] addresses = { "Malchei Israel 12", "Minchat Itzchak 23", "Hashofar 3", "Hare'em 14", "Eben Ezra 6", "HaTzvi 4", "Hakfir 5", "Hadishon 6", "Ha'ari 13", "Hatan 17", "Tchelet Mordechei 18", "Hamarkalim 4", "Rabi Akiva 15", "Hazon Ish 53", "Harav Shach 3" };
//        (double Latitude, double Longitude)[] coordinates = {(31.771959, 35.217018 ), (  31.775469, 35.229568 ),  (  31.774578, 35.207031 ),
//  (  31.780125,  35.207889 ),  (  31.792178,  35.205321 ), (  31.784222,  35.213127 ), (  31.797433,  35.214278 ), (  31.798189,  35.206539 ),
//   (  31.794056,  35.221325 ),(  31.798832,  35.214994 ),(  31.799845,  35.227146 ), (  31.780829,  35.229744 ),
//  (31.781869,  35.236349 ),(  31.799467,  35.240458 ), (  31.788469,  35.235679 )};
//        string password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D88";
//        int[] maxDistances = { 219, 748, 298, 767, 893, 419, 974, 908, 959, 450, 166, 978, 595, 250, 575 };
//        s_dal!.Volunteer.Create(new(328118245, "Malka", "0583278625", "b7malka@gmail.com", password, "Man 3", 31.8, 35.1, Role.ADMINISTRATOR, true, 250, (TypeOfDistance)s_rand.Next(0, 2)));

//        for (int i = 0; i < 15; i++)
//        {
//            s_dal!.Volunteer.Create(new(Ids[i], names[i], phones[i], $"{names[i]}{phones[i]}@gmail.com", password, addresses[i], coordinates[i].Latitude, coordinates[i].Longitude, Role.STANDARD, true, maxDistances[i], (TypeOfDistance)s_rand.Next(0, 2)));
//        }

//    }
//    private static void CreateCall()
//    {
//        string[] addresses = { "Herzl St, Tel Aviv", "Jaffa St, Jerusalem", "Ben Gurion Blvd, Haifa", "Main St, Beersheba", "Eilat Promenade, Eilat", "Rothschild Blvd, Tel Aviv", "Dizengoff St, Tel Aviv", "Ben Yehuda St, Tel Aviv", "Shderot Ben Gurion, Netanya", "David HaMelech St, Herzliya" };
//        (double Latitude, double Longitude)[] coordinates =
//        { (32.0853, 34.7818), (31.7683, 35.2137), (32.7940, 34.9896), (31.2518, 34.7913), (29.5581, 34.9482), (32.0656, 34.7770), (32.0755, 34.7756), (32.0838, 34.7698), (32.3320, 34.8599), (32.1673, 34.8360) };
//        string[] descriptions =
//        {
//        "Flat tire on the highway.",
//        "Car battery is dead, needs jump start.",
//        "Ran out of fuel near the city center.",
//        "Locked keys inside the vehicle.",
//        "Vehicle recovery required from the beach.",
//        "Minor mechanical issue near the mall.",
//        "Medical emergency near the park.",
//        "Car stuck in the sand, needs assistance.",
//        "Flat tire on the road to Jerusalem.",
//        "Jump start required at the parking lot."
//    };

//        // בדיקה ששעון המערכת מאותחל
//        if (s_dal.Config.Clock == default)
//            throw new Exception("Clock value is not initialized in configuration.");
//        int hour = s_dal.Config.Clock.Hour >= 7 ? s_dal.Config.Clock.Hour - 7 : 0;
//        DateTime start = new DateTime(
//            s_dal.Config.Clock.Year,
//            s_dal.Config.Clock.Day,
//            s_dal.Config.Clock.Month,
//            hour, 0, 0);

//        int range = (int)(s_dal.Config.Clock - start).TotalMinutes;
//        if (range <= 0)
//            throw new Exception("Invalid time range for creating calls.");

//        for (int i = 0; i < 50; i++)
//        {
//            int startingTime = s_rand.Next(range);
//            int index = s_rand.Next(addresses.Length);
//            Call newCall = new Call(
//                0,
//                (TypeOfCall)s_rand.Next(Enum.GetValues(typeof(TypeOfCall)).Length),
//                descriptions[s_rand.Next(descriptions.Length)],
//                addresses[index],
//                coordinates[index].Latitude,
//                coordinates[index].Longitude,
//                s_dal.Config.RiskRange,
//                                start.AddMinutes(startingTime + s_rand.Next(30, 360)),
//                CallStatus.OPEN,
//start.AddMinutes(startingTime));

//            // יצירה ב-DAL
//            s_dal.Call.Create(newCall);
//        }
//    }

//    private static void CreateAssignment()
//    {
//        List<Call>? callsList = s_dal!.Call!.ReadAll().ToList();
//        List<Volunteer>? volunteersList = s_dal.Volunteer!.ReadAll().ToList();

//        for (int i = 0; i < 50; i++)
//        {
//            // חישוב הזמן ההתחלתי והזמן הסופי עבור כל קריאה
//            DateTime minTime = callsList[i].OpeningTime; // הזמן המינימלי
//            DateTime maxTime = (DateTime)callsList[i].MaxClosingTime!; // הזמן המקסימלי
//            //TimeSpan diff = maxTime - minTime - TimeSpan.FromHours(2);
//            TimeSpan diff = maxTime - minTime - TimeSpan.FromHours(2);
//            if (diff.TotalMinutes <= 0)
//            {
//                diff = TimeSpan.FromMinutes(1); // ערך ברירת מחדל למניעת שגיאה
//            }
//            DateTime randomTime = minTime.AddMinutes(s_rand.Next((int)diff.TotalMinutes));
//            s_dal!.Assignment.Create(new Assignment(0, callsList[i].Id, volunteersList[s_rand.Next(volunteersList.Count())].Id, randomTime, randomTime.AddHours(2), (TypeOfTreatmentEnding)s_rand.Next(Enum.GetValues(typeof(TypeOfTreatmentEnding)).Length), AssignmentStatus.OPEN));
//        }
//    }

//    public static void Do() //stage 1
//    {
//        //s_dal = dal ?? throw new NullException("DAL object can not be null!");
//        s_dal = DalApi.Factory.Get; //stage 4


//        Console.WriteLine("Reset Configuration values and List values...");
//        s_dal.ResetDB();

//        Console.WriteLine("Initializing Volunteers list ...");
//        Console.WriteLine("Initializing Calls list ...");
//        Console.WriteLine("Initializing Assignments list ...");

//        CreateVolunteer();
//        CreateCall();
//        CreateAssignment();
//    }
//}
namespace DalTest;
using DalApi;
using DO;



public static class Initialization
{
    private static IDal? s_dal;
    private static readonly Random s_rand = new();
    private static void CreateVolunteer()
    {
        int[] Ids = { 123456782, 204589326, 315869241, 428193657, 537921483, 648291375, 759183624, 860195372, 972843165, 183726495, 294817536, 305918742, 416293857, 527384961, 638475120 };
        string[] names = { "Tzipy", "Chani", "Yehudit", "Shira", "Moishy", "Efraim", "Shloimy", "Yosi", "Yoni", "Avi", "Chaim", "Ester", "Rachel", "Shani", "Michali" };
        string[] phones = { "0527614421", "0527614422", "0527614423", "0527614424", "0527614425", "0527614426", "0527614427", "0527614428", "0527614429", "0527614430", "0527614431", "0527614432", "0527614433", "0527614434", "0527614435" };
        string[] addresses = { "Malchei Israel 12", "Minchat Itzchak 23", "Hashofar 3", "Hare'em 14", "Eben Ezra 6", "HaTzvi 4", "Hakfir 5", "Hadishon 6", "Ha'ari 13", "Hatan 17", "Tchelet Mordechei 18", "Hamarkalim 4", "Rabi Akiva 15", "Hazon Ish 53", "Harav Shach 3" };
        (double Latitude, double Longitude)[] coordinates = {(31.771959, 35.217018 ), (  31.775469, 35.229568 ),  (  31.774578, 35.207031 ),
  (  31.780125,  35.207889 ),  (  31.792178,  35.205321 ), (  31.784222,  35.213127 ), (  31.797433,  35.214278 ), (  31.798189,  35.206539 ),
   (  31.794056,  35.221325 ),(  31.798832,  35.214994 ),(  31.799845,  35.227146 ), (  31.780829,  35.229744 ),
  (31.781869,  35.236349 ),(  31.799467,  35.240458 ), (  31.788469,  35.235679 )};
        string password = "E86D1251A41C5FFAD8CD240F989A32EA1251B468C4E866EA86EAEA2BF40A2259";
        int[] maxDistances = { 219, 748, 298, 767, 893, 419, 974, 908, 959, 450, 166, 978, 595, 250, 575 };
        s_dal!.Volunteer.Create(new(328118245, "Malka", "0583278625", "b7malka@gmail.com", password, "Man 3", 31.8, 35.1, Role.ADMINISTRATOR, true, 250, (TypeOfDistance)s_rand.Next(0, 2)));

        for (int i = 0; i < 15; i++)
        {
            s_dal!.Volunteer.Create(new(Ids[i], names[i], phones[i], $"{names[i]}{phones[i]}@gmail.com", password, addresses[i], coordinates[i].Latitude, coordinates[i].Longitude, Role.STANDARD, true, maxDistances[i], (TypeOfDistance)s_rand.Next(0, 2)));
        }

    }
    //private static void CreateCall()
    //{
    //    //    string[] addresses = { "Herzl St, Tel Aviv", "Jaffa St, Jerusalem", "Ben Gurion Blvd, Haifa", "Main St, Beersheba", "Eilat Promenade, Eilat", "Rothschild Blvd, Tel Aviv", "Dizengoff St, Tel Aviv", "Ben Yehuda St, Tel Aviv", "Shderot Ben Gurion, Netanya", "David HaMelech St, Herzliya" };
    //    (double Latitude, double Longitude)[] coordinates =
    //    { (32.0853, 34.7818), (31.7683, 35.2137), (32.7940, 34.9896), (31.2518, 34.7913), (29.5581, 34.9482), (32.0656, 34.7770), (32.0755, 34.7756), (32.0838, 34.7698), (32.3320, 34.8599), (32.1673, 34.8360) };
    //    string[] descriptions =
    //    {
    //    "Flat tire on the highway.",
    //    "Car battery is dead, needs jump start.",
    //    "Ran out of fuel near the city center.",
    //    "Locked keys inside the vehicle.",
    //    "Vehicle recovery required from the beach.",
    //    "Minor mechanical issue near the mall.",
    //    "Medical emergency near the park.",
    //    "Car stuck in the sand, needs assistance.",
    //    "Flat tire on the road to Jerusalem.",
    //    "Jump start required at the parking lot."
    //};

    //    // בדיקה ששעון המערכת מאותחל
    //    if (s_dal.Config.Clock == default)
    //        throw new Exception("Clock value is not initialized in configuration.");
    //    int hour = s_dal.Config.Clock.Hour >= 7 ? s_dal.Config.Clock.Hour - 7 : 0;
    //    DateTime start = new DateTime(
    //        s_dal.Config.Clock.Year,
    //        s_dal.Config.Clock.Day,
    //        s_dal.Config.Clock.Month,
    //        hour, 0, 0);

    //    int range = (int)(s_dal.Config.Clock - start).TotalMinutes;
    //    if (range <= 0)
    //        throw new Exception("Invalid time range for creating calls.");

    //    for (int i = 0; i < 50; i++)
    //    {
    //        int startingTime = s_rand.Next(range);
    //        int index = s_rand.Next(addresses.Length);
    //        Call newCall = new Call(
    //            0,
    //            (TypeOfCall)s_rand.Next(Enum.GetValues(typeof(TypeOfCall)).Length),
    //            descriptions[s_rand.Next(descriptions.Length)],
    //            addresses[index],
    //            coordinates[index].Latitude,
    //            coordinates[index].Longitude,
    //            s_dal.Config.RiskRange,
    //            start.AddMinutes(startingTime),
    //            CallStatus.OPEN,
    //            start.AddMinutes(startingTime + s_rand.Next(30, 360))
    //        );

    //        // יצירה ב-DAL
    //        s_dal.Call.Create(newCall);
    //    }
    public static string[] CallAddresses =
{

    "Meah Shearim St 10, Jerusalem", "Chazon Ish St 6, Jerusalem", "Ramat Eshkol St 11, Jerusalem",
    "Har Safra St 1, Jerusalem", "Mount Scopus St 4, Jerusalem", "Keren Hayesod St 30, Jerusalem",
    "Neve Yaakov St 17, Jerusalem", "Shmuel HaNavi St 12, Jerusalem", "Yechiel St 3, Jerusalem",
    "Rav Kook St 4, Jerusalem", "Talmud Torah St 8, Jerusalem", "Sanhedria St 18, Jerusalem",
    "Kiryat Moshe St 6, Jerusalem", "Achad Ha'am St 2, Jerusalem", "Bar Ilan St 7, Jerusalem",
    "City Center St 14, Jerusalem", "Rechov Yechiel 3, Jerusalem", "Giv'at Shaul St 7, Jerusalem",
    "Nachlaot St 7, Jerusalem", "Rav Kook St 5, Jerusalem", "Har Nof St 18, Jerusalem",
    "Ramat Shlomo St 15, Jerusalem", "Sderot Yitzhak Rabin St 5, Jerusalem", "Har Hatzofim St 8, Jerusalem",
    "Giv'at HaMivtar St 6, Jerusalem", "Tefilat Yisrael St 14, Jerusalem", "Malkhei Yisrael St 10, Jerusalem",
    "Kiryat Tzahal St 6, Jerusalem", "Nachal Noach St 17, Jerusalem", "Maalot Dafna St 6, Jerusalem",
    "Har HaMor St 3, Jerusalem", "Ramat HaSharon St 2, Jerusalem", "Yakar St 3, Jerusalem",
    "Rav Haim Ozer St 9, Jerusalem", "Yehoshua Ben-Nun St 5, Jerusalem", "Meir Schauer St 12, Jerusalem",
    "Menachem Begin St 11, Jerusalem", "Yisrael Yaakov St 13, Jerusalem", "Ben Yehuda St 6, Jerusalem",
    "Hadar St 3, Jerusalem", "Maharal St 8, Jerusalem", "Yosef Schwartz St 4, Jerusalem",
    "Jabotinsky St 7, Jerusalem", "Shazar St 5, Jerusalem", "Gonenim St 12, Jerusalem",
    "Talpiot St 14, Jerusalem", "Bilu St 9, Jerusalem", "Yovel St 2, Jerusalem",
    "Herzl St 3, Jerusalem", "Hashmonai St 6, Jerusalem", "Ramot St 17, Jerusalem",
    "Shalom Aleichem St 10, Jerusalem", "Eli Cohen St 4, Jerusalem", "Shlomo HaMelech St 7, Jerusalem"
    };

    public static double[] CallLatitudes = {
    31.7865608, 31.759595, 31.8017893, 31.759595, 31.794767,
    31.7723879, 31.842212, 31.7875022, 31.7472349, 31.7831088,
    31.898714, 31.8051921, 31.7857651, 31.759595, 31.7957696,
    31.7259643, 31.7476677, 31.759595, 31.703275, 31.7831088,
    31.7858115, 31.8111253, 31.759595, 31.759595, 31.759595,
    31.779042, 31.7909196, 32.0416824, 31.759595, 31.7933736,
    31.7854673, 32.3260388, 31.8405465, 31.759595, 31.759595,
    31.759595, 31.7686856, 31.8383352, 31.7815767, 32.8089768,
    31.7215207, 31.759595, 31.7709719, 31.7135737, 31.906037,
    31.7515394, 31.762803, 31.780514, 31.74948, 31.77676,
    31.81561, 31.7723328, 31.7668532, 31.759595
    };


    public static double[] CallLongitudes = {
    35.2208052, 35.215315, 35.2228759, 35.215315, 35.2425346,
    35.2215257, 35.24206, 35.2265973, 35.2326395, 35.2203032,
    35.185758, 35.2157309, 35.1968887, 35.215315, 35.2198077,
    34.7437502, 35.2323345, 35.215315, 35.194809, 35.2203032,
    35.1741509, 35.2174861, 35.215315, 35.215315, 35.215315,
    35.229702, 35.2089577, 34.7904787, 35.215315, 35.2246764,
    35.1001866, 34.8511049, 35.2454408, 35.215315, 35.215315,
    35.215315, 35.1950858, 35.2441809, 35.2180856, 34.997939,
    35.2284413, 35.215315, 35.2210202, 34.9838857, 35.203005,
    35.2160228, 35.2099602, 35.217981, 34.9880954, 35.230342,
    35.1954938, 35.2215927, 35.213483, 35.215315
    };

    public static string[] CallDescriptions =
    {
            // High Urgency (Immediate and Critical):
            "Medical Emergency", "Accident Assistance", "Vehicle Recovery", "Emergency Towing", "Stuck in Mud",
            "Engine Overheating", "Heavy Accident", "Emergency Tow", "Battery Issues", "Fire in Vehicle",

            // Medium Urgency (Urgent but less critical):
            "Fuel Assistance", "Flat Tire", "Lockout Assistance", "Vehicle Breakdown", "Flat Battery",
            "Vehicle Stuck in Traffic", "Accident Help", "Lost Keys", "Driving Assistance", "Tire Puncture",
   
            // General Assistance (Everyday issues or less urgent):
            "Driving Safety", "Parking Assistance", "Car Repair Advice", "Vehicle Inspection", "Insurance Assistance",
            "Vehicle Jumpstart", "Call for Help", "Car Service Appointment", "Windshield Repair", "Lost Wallet",
            "Fuel Running Low", "Battery Jumpstart", "Car Alarm Issues", "Motorcycle Breakdown", "Lost GPS Navigation",
            "Roadside Assistance", "Headlight Malfunction", "Engine Check", "Car Wash", "Flat Tire Change",

            // General or Non-Urgent Assistance:
            "Road Advice", "Car Repair Consultation", "Driving Safety Tips", "Insurance Policy Questions", "Basic Vehicle Maintenance",
            "Navigation Assistance", "Towing Insurance", "Tire Pressure Check", "Late Night Assistance", "General Car Troubleshooting"
    };


    public static void CreateCall()
    {
        Random rand = new Random();
        DateTime now = DateTime.Now;
        DateTime twoHoursAgo = now.AddHours(-2);
        int totalMinutesRange = (int)(now - twoHoursAgo).TotalMinutes;
        int randomMinutes = rand.Next(totalMinutesRange);
        DateTime MyStartTime = twoHoursAgo.AddMinutes(randomMinutes);

        for (int i = 0; i < 50; i++)
        {
            string MyDescription = CallDescriptions[i];
            string MyAddress = CallAddresses[i];
            double MyLatitude = CallLatitudes[i];
            double MyLongitude = CallLongitudes[i];
            DateTime MyExpiredTime;
            TypeOfCall MyCallType;

            switch (i)
            {
                case int n when (n < 10):
                    MyCallType = TypeOfCall.CARDIAC_ARREST;
                    MyExpiredTime = MyStartTime.AddDays(15);
                    break;

                case int n when (n >= 10 && n < 20):
                    MyCallType = TypeOfCall.ALLERGIC_REACTION;
                    MyExpiredTime = MyStartTime.AddDays(30);
                    break;

                case int n when (n >= 20 && n < 40):
                    MyCallType = TypeOfCall.CHILDBIRTH;
                    MyExpiredTime = MyStartTime.AddYears(1);
                    break;

                case int n when (n >= 40 && n < 50):
                    MyCallType = TypeOfCall.POISONING;
                    MyExpiredTime = MyStartTime.AddMonths(2);
                    break;

                default:
                    return;
            }

            Call call = new Call
            {
                Id = 0,
                Description = MyDescription,
                Address = MyAddress,
                latitude = MyLatitude,
                longitude = MyLongitude,
                OpeningTime = MyStartTime,
                MaxClosingTime = MyExpiredTime,
                TypeOfCall = MyCallType,
            };

            s_dal.Call.Create(call);
        }
    }


    //private static void CreateAssignment()
    //{
    //    List<Call>? callsList = s_dal!.Call!.ReadAll().ToList();
    //    List<Volunteer>? volunteersList = s_dal.Volunteer!.ReadAll().ToList();

    //    for (int i = 0; i < 50; i++)
    //    {
    //        // חישוב הזמן ההתחלתי והזמן הסופי עבור כל קריאה
    //        DateTime minTime = callsList[i].OpeningTime; // הזמן המינימלי
    //        DateTime maxTime = (DateTime)callsList[i].MaxClosingTime!; // הזמן המקסימלי
    //        //TimeSpan diff = maxTime - minTime - TimeSpan.FromHours(2);
    //        TimeSpan diff = maxTime - minTime - TimeSpan.FromHours(2);
    //        if (diff.TotalMinutes <= 0)
    //        {
    //            diff = TimeSpan.FromMinutes(1); // ערך ברירת מחדל למניעת שגיאה
    //        }
    //        DateTime randomTime = minTime.AddMinutes(s_rand.Next((int)diff.TotalMinutes));
    //        s_dal!.Assignment.Create(new Assignment(0, callsList[i].Id, volunteersList[s_rand.Next(volunteersList.Count())].Id, randomTime, randomTime.AddHours(2), (TypeOfTreatmentEnding)s_rand.Next(Enum.GetValues(typeof(TypeOfTreatmentEnding)).Length), AssignmentStatus.OPEN));
    //    }

    private static void CreateAssignment()
    {
        List<Volunteer>? volunteers = s_dal!.Volunteer.ReadAll().ToList(); ;
        IEnumerable<Call>? calls = s_dal!.Call.ReadAll();

        for (int i = 0; i < 50; i++)
        {
            DateTime minTime = calls.ElementAt(i).OpeningTime;
            DateTime maxTime = (DateTime)calls.ElementAt(i).MaxClosingTime!;
            TimeSpan difference = maxTime - minTime - TimeSpan.FromHours(2);

            int validDifference = (int)Math.Max(difference.TotalMinutes, 0);
            DateTime randomTime = minTime.AddMinutes(s_rand.Next(validDifference));
            if (i < 25)
            {
                //s_dal!.Assignment.Create(new Assignment(0, calls.ElementAt(i).Id, volunteers[s_rand.Next(volunteers.Count)].Id, randomTime, randomTime.AddHours(2)), (TypeOfTreatmentEnding)s_rand.Next(Enum.GetValues(typeof(TypeOfTreatmentEnding)).Length - 1),AssignmentStatus.BEING_HANDELED);
                s_dal!.Assignment.Create(new Assignment(0, calls.ElementAt(i).Id, volunteers[s_rand.Next(volunteers.Count)].Id, randomTime, randomTime.AddHours(2), (TypeOfTreatmentEnding)s_rand.Next(Enum.GetValues(typeof(TypeOfTreatmentEnding)).Length - 1), AssignmentStatus.BEING_HANDELED));

            }
            else
            {
                s_dal!.Assignment.Create(new Assignment(0, calls.ElementAt(i).Id, volunteers[s_rand.Next(volunteers.Count)].Id, randomTime, randomTime.AddHours(3), (TypeOfTreatmentEnding)s_rand.Next(Enum.GetValues(typeof(TypeOfTreatmentEnding)).Length - 1), AssignmentStatus.OPEN));
            }
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

