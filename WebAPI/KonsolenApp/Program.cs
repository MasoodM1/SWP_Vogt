using ArticleLibrary;
using System.Net.Http.Json;

namespace KonsolenApp
{
    internal class Program
    {
        private static HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            // Basisadresse für alle folgenden HTTP-Abfragen
            _httpClient.BaseAddress = new Uri("https://localhost:7204/api/shop/");

            // Ziel: alle Artikel von der WebAPI ermitteln und anzeigen
            //      - wir erhalten Daten im Json-Format
            //      - umwandeln nach List<Article> + anzeigen

            await GetAllArticles();
            await GetArticleById(2);
            await DeleteArticleById(2);
            await CreateArticle(new Article { Name = "Samsung Galaxy S24", Preis = 199.99 });
            await UpdateArticle(1, new Article { Name = "Pixel 9", Preis = 899.99 });
            await GetAllArticles();

            Console.ReadKey();
        }

        public static async Task<List<Article>> GetAllArticles()
        {
            List<Article>? articles = await _httpClient.GetFromJsonAsync<List<Article>>("articles?apiKey=302f88d2-2278-4e9d-b4cd-c3929902383a");

            if (articles != null)
            {
                articles.ForEach(a => Console.WriteLine(a));
            }
            return articles;
        }

        public static async Task<Article?> GetArticleById(int id)
        {
            Article? article = await _httpClient.GetFromJsonAsync<Article>($"articles/{id}?apiKey=302f88d2-2278-4e9d-b4cd-c3929902383a");

            if (article != null)
            {
                Console.WriteLine(article);
            }
            return article;
        }

        public static async Task DeleteArticleById(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"articles/{id}?apiKey=302f88d2-2278-4e9d-b4cd-c3929902383a");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artikel wurde gelöscht");
            }
            else
            {
                Console.WriteLine("Artikel konnte nicht gelöscht werden");
            }
        }

        public static async Task CreateArticle(Article article)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("articles?apiKey=302f88d2-2278-4e9d-b4cd-c3929902383a", article);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artikel wurde angelegt");
            }
            else
            {
                Console.WriteLine("Artikel konnte nicht angelegt werden");
            }
        }

        public static async Task UpdateArticle(int id, Article article)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"articles/{id}?apiKey=302f88d2-2278-4e9d-b4cd-c3929902383a", article);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artikel wurde geändert");
            }
            else
            {
                Console.WriteLine("Artikel konnte nicht geändert werden");
            }
        }
    }
}
