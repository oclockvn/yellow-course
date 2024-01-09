namespace SavePersonInfo
{
    public class PersonService
    {
        public static void WriteToFile(List<Person> people, string folder)
        {
            if (people == null || people.Count == 0) return;    

            // {folder}/{james}/{james}.txt
            foreach (Person person in people)
            {
                var output = Path.Combine(folder, person.Name);
                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output); // folder/james
                }

                var path = Path.Combine(output, person.Name + ".txt"); // folder/james/james.txt
                File.WriteAllText(path, $"{person.Name} is {person.Age} years old");
            }
        }

        public static List<Person> ReadFile(string path)
        {
            // check input
            ArgumentNullException.ThrowIfNullOrWhiteSpace(path);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            // read all lines
            var lines = File.ReadAllLines(path);
            if (lines is null || lines.Length == 0)
            {
                return [];
            }

            /*
            [
              {
                "id": 1,
                "name": "John",
                "age": 18
              },
              {
                "id": 2,
                "name": "James",
                "age": 20
              }
            ]
             */

            List<Person> people = [];
            Person p = new();

            string current;
            foreach (var line in lines)
            {
                current = line.Trim();

                if (current.StartsWith("\"name\""))
                {
                    p = new Person();
                    p.Name = GetValue(current);
                }
                else if (current.StartsWith("\"age\""))
                {
                    p.Age = int.Parse(GetValue(current));
                    people.Add(p);
                }
            }

            return people;
        }

        public static string GetValue(string input)
        {
            //  "name": "John",
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var arr = input
                .Trim() // "  xxx:aaa,   " -> "xxx:aaa,"
                .TrimEnd(',') // "xxx:aaa," -> "xxx:aaa"
                .Split(':'); // "xxx:aaa" -> ["xxx", "aaa"]

            var value = arr.Last()
                .Trim()
                .Trim('"'); // " \"James\" "

            return value;
        }
    }
}
