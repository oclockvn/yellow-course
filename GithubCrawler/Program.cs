using GithubCrawler.Services;

namespace GithubCrawler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter repo name: ");
            var repoName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(repoName))
            {
                Console.WriteLine("Exit!");
                return;
            }

            // search repos by name
            var repos = GitHubSearchService.SearchRepos(repoName);
            if (repos is null || repos.Count == 0)
            {
                Console.WriteLine($"No repo found by name {repoName}");
                return;
            }

            // write repos to file
            GitHubFileService.WriteRepos(repos, "Output");

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
