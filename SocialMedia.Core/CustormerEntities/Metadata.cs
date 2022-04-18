using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.CustormerEntities
{
    public class Metadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextiusPages { get; set; }
        public bool HasPreviusPages { get; set; }
        public string NextiusPagesUrl { get; set; }
        public string PreviusPagesUrl { get; set; }
    }
}