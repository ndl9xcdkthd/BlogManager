using System;

namespace BlogManager.Application.Features.Brands.Queries.GetById
{
    public class GetBrandByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}