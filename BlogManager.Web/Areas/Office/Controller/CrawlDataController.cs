using BlogManager.Web.Abstractions;
using BlogManager.Web.Areas.Office.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogManager.Web.Areas.Office.Controller
{
    [Area("Office")]
    public class CrawlDataController : BaseController<CrawlDataController>
    {
        public IActionResult Index()
        {
            var viewmodel = new ListCrawlDataViewModel();
            return View(viewmodel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(string url)
        {
            
            string[] between = url.Split('\r','\n');
            

            var addUrl = new List<ListUrl>();

            var crawlData = new List<CrawlDataViewModel>();

            foreach (var urlSon in between)
            {
                if(urlSon == "")
                {
                    continue;
                }
                else
                {
                    var viewListUrl = new ListUrl
                    {
                        Url = urlSon
                    };
                    addUrl.Add(viewListUrl);
                }
            }


            foreach (var cr in addUrl)
            {
                HttpClient hc = new HttpClient();

                HttpResponseMessage result = await hc.GetAsync(cr.Url);

                Stream stream = await result.Content.ReadAsStreamAsync();

                HtmlDocument document = new HtmlDocument();

                document.Load(stream);

                var content =  document.DocumentNode.SelectSingleNode("/html/body");

                var title = document.DocumentNode.SelectSingleNode("/html/head/title");

                HtmlNodeCollection link = document.DocumentNode.SelectNodes("/html/head/link");

                var addLink = new List<ListLink>();

                foreach (var item in link)
                {
                    var links = "<link href=" + item.Attributes["href"].Value + "/> \n";

                    var viewListLink = new ListLink
                    {
                        Link = links
                    };

                    addLink.Add(viewListLink);
                }

                var viewLisCrawlData = new CrawlDataViewModel
                {
                    Content = content.InnerHtml,
                    Url = cr.Url,
                    Title = title.InnerText,
                    Links = addLink
                };
                crawlData.Add(viewLisCrawlData);
            }
            
            var listCrawlData = new ListCrawlDataViewModel { CrawlDatas = crawlData };

            return View(listCrawlData);
        }
    }
}
