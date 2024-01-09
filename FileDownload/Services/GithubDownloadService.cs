namespace FileDownload.Services
{
    public class GitHubDownloadService
    {
        /// <summary>
        /// download file https://raw.githubusercontent.com/oclockvn/yellow-course/master/YellowCourse.sln
        /// </summary>
        /// <param name="url"></param>
        /// <param name="output">files/2024/01/</param>
        public static async Task<string> DownloadFileAsync(string url, string output)
        {
            // download file from url
            using var httpClient = new HttpClient();
            var bytes = await httpClient.GetByteArrayAsync(url);

            // file not found or something
            if (bytes is null || bytes.Length == 0)
            {
                return string.Empty;
            }

            // save file to output folder
            // https://raw.githubusercontent.com/oclockvn/yellow-course/master/YellowCourse.sln
            // YellowCourse.sln
            if (!Directory.Exists(output))
            {
                try
                {
                    Directory.CreateDirectory(output);
                }
                catch (Exception)
                {
                    await Console.Out.WriteLineAsync("Could not create folder");

                    return string.Empty;
                }
            }

            var filename = Path.GetFileName(url); // YellowCourse.sln
            var path = Path.Combine(output, filename); // files/2024/01/YellowCourse.sln
            await File.WriteAllBytesAsync(path, bytes);

            return path;
        }
    }
}
