using System.Net.Http;

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

        public void OnGet()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,
                "http://www.tsetmc.com/Loader.aspx?ParTree=15131F");
        }
    }
}
