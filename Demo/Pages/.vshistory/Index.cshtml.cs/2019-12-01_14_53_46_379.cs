using System;
using System.Net.Http;
using System.Text;
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

                    const string pageURL = "http://html-agility-pack.net/";
                    HttpClient httpClient = new HttpClient();
                    HttpResponseMessage responsee = await httpClient.GetAsync(pageURL);
                    string pageContents = await responsee.Content.ReadAsStringAsync();
                    byte[] buf = await client.GetByteArrayAsync("https://jackslater.ir/");
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Encoding enc1252 = Encoding.GetEncoding(1252);
                    Encoding enc1252_2 = CodePagesEncodingProvider.Instance.GetEncoding(1252);
                    string html = enc1252.GetString(buf);
                    string html_2 = enc1252_2.GetString(buf);
                    HtmlDocument pageDocument = new HtmlDocument();
                    pageDocument.LoadHtml(html);
                    pageDocument.Load(responseStream);
                    //string headlineText = pageDocument.DocumentNode
                    //    //.SelectSingleNode("(//div[contains(@class,'pb-f-homepage-hero')]//h3)[1]").InnerText;
                    //    .SelectSingleNode("(//div[contains(@class,'secSep')]//)").InnerText;
                    //var champWinRate = pageDocument.DocumentNode.SelectSingleNode("//*[@id=\"SummonerLayoutContent\"]/div[2]/div[1]/div[2]/div[2]/div[1]/div/div[1]/div[4]/div[2]");
                    HtmlNode node = pageDocument.DocumentNode.SelectSingleNode("//body");

                    HtmlWeb web = new HtmlWeb
                    {
                        OverrideEncoding = Encoding.Unicode
                    };
                    web.OverrideEncoding = Encoding.GetEncoding("iso-8859-1");
                    HtmlDocument document = await web.LoadFromWebAsync(pageURL);
                    HtmlNode red = document.DocumentNode.SelectSingleNode("//head/title");
                    Console.WriteLine("Node Name: " + red.Name + "\n" + red.OuterHtml);
                    string allItems = document.GetElementbyId("scroll-to-top").OuterHtml;

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
