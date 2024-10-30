partial class Program
{
    private static void Main(string[] args)
    {
        welcome1515();
        welcome8245();
        Console.ReadKey();
    }
    static partial void welcome8245();
    private static void welcome1515()
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", name);
    }
}