using System;
using System.Collections.Generic;
using System.Linq;
namespace SocialMedia.Core.CustormerEntities
{
    public  class PagedList<T>: List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        //navegacion entre paginas
        public bool HasPreviusPages => CurrentPage > -1;
        public bool HasNextiusPages => CurrentPage < TotalPages;
        public int? NextPageNumber=> HasNextiusPages ? CurrentPage +1 : (int?)null;
        public int? PreviusPageNumber=>HasPreviusPages ? CurrentPage - 1 : (int?)null;
        public PagedList(List<T> items,int count, int pageNumer, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumer;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            AddRange(items);
        }
        public static PagedList<T> create(IEnumerable<T> sourse, int pageNumber, int pageSize)
        {
            var count = sourse.Count();
            var items= sourse.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}