using GithubCrawler.Models;

namespace GithubCrawler.Services
{
    public class GitHubFileService
    {
        public static void WriteRepos(List<GithubRepo> repos, string folder)
        {
            if (repos is null || repos.Count == 0)
            {
                throw new ArgumentException("No repo provided");
            }

            ArgumentException.ThrowIfNullOrWhiteSpace(folder);

            foreach (var repo in repos)
            {
                var dir = repo.Name.Split('/');
                var output = Path.Combine(folder, dir[0]);
                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output);
                }

                var fullPath = Path.Combine(output, $"{dir[1]}.txt");
                File.WriteAllText(fullPath, $"repo {dir[1]} has {repo.Star} stars, {repo.Watch} watch and {repo.Fork} forks");
            }
        }
    }
}
