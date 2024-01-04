namespace FileHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var email = ReadingLargeFile.GetFirstEmailUsingStream("sample_data.csv", 90);
            Console.WriteLine(email);
            Console.ReadKey();
        }
    }
}
