using FileDownload.Services;

namespace FileDownload
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            var output = Path.Combine("files", DateTime.Now.Year.ToString(), DateTime.Now.ToString("MM"));
            var file = await GitHubDownloadService.DownloadFileAsync(
                "https://raw.githubusercontent.com/oclockvn/yellow-course/master/YellowCourse.sln",
                output);

            var absolutePath = Path.GetFullPath(file);
            await Console.Out.WriteLineAsync($"File is saved at {absolutePath}");
        }
    }
}
