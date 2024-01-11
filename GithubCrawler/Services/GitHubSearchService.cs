using GithubCrawler.Models;

namespace GithubCrawler.Services
{
    public class GitHubSearchService
    {
        public static async Task<List<GithubRepo>> SearchRepos(string search, int take = 10)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(search);

            // todo: encode whitespace name

            // request to github search api
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");

            /*
             {
                  "total_count": 162,
                  "incomplete_results": false,
                  "items": [
                    {
                      "id": 198028350,
                      "node_id": "MDEwOlJlcG9zaXRvcnkxOTgwMjgzNTA=",
                      "name": "Coursera_Yellow-Belt",
                    },
                    {},
                    {}
                  ]
              }
            */
            var response = await httpClient.GetStringAsync($"https://api.github.com/search/repositories?q={search}");
            if (string.IsNullOrWhiteSpace(response))
            {
                return [];
            }

            var lines = response.Split("\n");
            List<GithubRepo> repos = [];
            GithubRepo repo = new();

            foreach (var line in lines)
            {
                if (GetValueFor("full_name", line, out var name))
                {
                    repo = new();
                    repo.Name = name;

                    if (repos.Count < take)
                    {
                        repos.Add(repo);
                    }
                    else
                    {
                        break;
                    }
                }
                else if (GetValueFor("html_url", line, out var url)) // todo: duplicate with owner url
                {
                    repo.Url = url;
                }
                else if (GetValueFor("description", line, out var desc))
                {
                    repo.Description = desc;
                }
                else if (GetValueFor("created_at", line, out var createdAt))
                {
                    repo.CreatedAt = createdAt;
                }
                else if (GetValueFor("stargazers_count", line, out var star))
                {
                    repo.Star = int.Parse(star);
                }
                else if (GetValueFor("watchers_count", line, out var watch))
                {
                    repo.Watch = int.Parse(watch);
                }
                else if (GetValueFor("forks", line, out var fork))
                {
                    repo.Fork = int.Parse(fork);
                }
            }

            return repos;
        }

        static bool GetValueFor(string name, string line, out string value)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            value = "";

            if (string.IsNullOrWhiteSpace(line))
            {
                return false;
            }

            if (line.Trim().StartsWith($"\"{name}\""))
            {
                value = GetValue(line);
                return true;
            }

            return false;
        }

        static string GetValue(string line)
        {
            //  "name": "Coursera_Yellow-Belt",
            return line
                .Trim() // remove whitespace
                .Trim(',') // remove ,
                .Split(':')
                .Last()
                .Trim()
                .Trim('"')
                .Trim();
        }
    }
}
