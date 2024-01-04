using System.Reflection;

namespace Week17_ClassLibraries
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string?[] list = ["", "", "a", "AA", null];
            var first = list.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));

            Console.WriteLine(first);
        }
    }
}
