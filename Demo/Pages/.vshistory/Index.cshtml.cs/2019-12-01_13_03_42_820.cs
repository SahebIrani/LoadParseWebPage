using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task OnGetAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,
                                            "http://www.tsetmc.com/Loader.aspx?ParTree=15131F");

            HttpClient client = ClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                //Branches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(responseStream);
            }
            else
            {
            }
        }
    }
}
