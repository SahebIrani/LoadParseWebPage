using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Demo.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            ClientFactory = clientFactory;
        }

        public IHttpClientFactory ClientFactory { get; }
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
                    Console.WriteLine(pageContents);
                    Console.ReadLine();




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
