using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobweb.Models
{
    public class Search
    {
        public IEnumerable<Listing> Listings { get; set; }
        public int ListingPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Listings.Count() / (double)ListingPerPage));
        }
        public IEnumerable<Listing> PaginatedBlogs()
        {
            int start = (CurrentPage - 1) * ListingPerPage;
            return Listings.OrderBy(b => b.id).Skip(start).Take(ListingPerPage);
        }
    }
}