using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using HtmlAgilityPack;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Demo.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory clientFactory, HttpClient httpClient)
        {
            _logger = logger;
            ClientFactory = clientFactory;
            HttpClient = httpClient;
        }

        public IHttpClientFactory ClientFactory { get; }
        public HttpClient HttpClient { get; }

        private readonly ILogger<IndexModel> _logger;

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "Loader.aspx?ParTree=15131F");

                HttpClient client = ClientFactory.CreateClient("tsetmc");

                HttpResponseMessage response = await client.SendAsync(request, cancellationToken);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    //Branches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(responseStream);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    string responseBody2 = await client.GetStringAsync("http://www.tsetmc.com/Loader.aspx?ParTree=15131F");

                    var responsee = await HttpClient.GetAsync("http://www.tsetmc.com/Loader.aspx?ParTree=15131F");
                    var pageContents = await responsee.Content.ReadAsStringAsync();
                    HtmlDocument pageDocument = new HtmlDocument();
                    pageDocument.LoadHtml(pageContents);

                    return new JsonResult(responseBody);
                }
                else
                {
                    string readResponse = await response.RequestMessage.Content.ReadAsStringAsync();
                    throw new HttpRequestException(readResponse);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return Page();
        }
    }
}
