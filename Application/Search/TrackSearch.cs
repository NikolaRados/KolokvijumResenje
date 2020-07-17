using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Search
{
    public class TrackSearch : PagedSearch
    {
        public string Name { get; set; }
    }
}
