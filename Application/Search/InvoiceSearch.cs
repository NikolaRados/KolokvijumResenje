using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Search
{
    public class InvoiceSearch : PagedSearch
    {
        public string Name { get; set; }
        public int? CustomerId { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public decimal? MinTotal { get; set; }
        public decimal? MaxTotal { get; set; }
    }
}
