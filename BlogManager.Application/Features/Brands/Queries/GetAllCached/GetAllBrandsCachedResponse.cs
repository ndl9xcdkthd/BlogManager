using System;

namespace BlogManager.Application.Features.Brands.Queries.GetAllCached
{
    public class GetAllBrandsCachedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}