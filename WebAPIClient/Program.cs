using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var repositories = await ProcessRepositories();

            foreach (var repo in repositories)
            //TODO попробовать заменить это на цикл по списку с properties
            {
                var name = repo.Name;
                var description = repo.Description;
                var url = repo.GitHubUrl;
                var homepage = repo.Homepage;
                var watchers = repo.Watchers;
                var push = repo.LastPush;
                Console.WriteLine($"Name of repository: {name}" +
                    $"\nDescription of repository: {description}" +
                    $"\nUrl of repository: {url}" +
                    $"\nHomepage of repository: {homepage}" +
                    $"\nWatchers of repository: {watchers}" +
                    $"\nLastPush of repository: {push}");
                Console.WriteLine();
                //Type type = repo.GetType();
                //PropertyInfo[] properties = type.GetProperties();

                //foreach (PropertyInfo property in properties)
                //{
                //    Console.WriteLine(property.GetValue(repo, null));
                //}
                //Console.WriteLine();
                //Console.WriteLine(repo.Name);
                //Console.WriteLine(repo.Description);
                //Console.WriteLine(repo.GitHubUrl);
                //Console.WriteLine(repo.Homepage);
                //Console.WriteLine(repo.Watchers);
                //Console.WriteLine(repo.LastPush);
                //Console.WriteLine();
            }
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            //var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            //var msg = await stringTask;
            //Console.Write(msg);
            //foreach (var repo in repositories)
            //Console.WriteLine(repo.Name);
            return repositories;

        }
    }
}
