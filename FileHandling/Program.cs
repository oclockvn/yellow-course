namespace FileHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "MOCK_DATA.csv";
            var lines = File.ReadAllLines(path);
            var result = new List<string>();

            foreach (var line in lines.Skip(1))
            {
                var arr = line.Split(','); // [1,Carny,Salerg,1991-04-30]
                if (!DateTime.TryParse(arr[3], out var date))
                {
                    continue;
                }

                var yearOld = DateTime.Now.Year - date.Year;
                if (yearOld < 18)
                {
                    continue;
                }

                result.Add($"{arr[1]} {arr[2]} is {yearOld} years old");
            }

            if (result.Count == 0)
            {
                return;
            }

            // write to file
            File.WriteAllLines($"output-{DateTime.Now:MMddhhmmss}.txt", result.ToArray());
        }
    }
}
