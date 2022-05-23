using System.Collections;
using System.Collections.Generic;

namespace BlogManager.Web.Areas.Catalog.Models
{
    public class ProductPage
    {
        public ProductPage()
        {

        }
        public IList<ProductViewModel> ProductItem { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalItem { get; set; }
    }
}
