namespace GithubCrawler.Models
{
    public struct GithubRepo
    {
        public string Name { get; set; }
        public string Url { get; set; } // html_url
        public string Description { get; set; } // description
        public string CreatedAt { get; set; } // created_at
        public int Star { get; set; } // stargazers_count
        public int Watch { get; set; } // watchers_count
        public int Fork { get; set; } // forks_count
    }
}
