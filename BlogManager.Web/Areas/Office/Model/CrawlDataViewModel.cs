using System.Collections.Generic;

namespace BlogManager.Web.Areas.Office.Models
{

    public class ListCrawlDataViewModel
    {
        public IList<CrawlDataViewModel> CrawlDatas { get; set; }
    }

    public class CrawlDataViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public IList<ListLink> Links { get; set; }
    }

    public class ListLink
    {
        public string Link { get; set; }
    }

    public class ListUrl
    {
        public string Url { get; set; }
    }

}
