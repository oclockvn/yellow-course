namespace SavePersonInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            var people = PersonService.ReadFile("people.txt");
            PersonService.WriteToFile(people, "output");

            Console.WriteLine("File is saved successfully");
            Console.ReadKey();
        }
    }
}
