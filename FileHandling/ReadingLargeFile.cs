namespace FileHandling
{
    public class ReadingLargeFile
    {
        /// <summary>
        /// Read file from given path and return the email of first record that has balance <= maxValue
        /// </summary>
        /// <param name="path"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static string? GetFirstEmail(string path, int maxValue)
        {
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                // 5,Dagny Chaudrelle,dchaudrelle4@cisco.com,94
                var arr = line.Split(',');
                var balance = int.Parse(arr.Last());
                if (balance <= maxValue)
                {
                    return arr[2];
                }
            }

            return null;
        }

        public static string? GetFirstEmailUsingStream(string path, int maxValue)
        {
            using var stream = new StreamReader(path);
            while (!stream.EndOfStream)
            {
                var line = stream.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var arr = line.Split(',');
                var balance = int.Parse(arr.Last());
                if (balance <= maxValue)
                {
                    return arr[2];
                }
            }

            return null;
        }
    }
}
