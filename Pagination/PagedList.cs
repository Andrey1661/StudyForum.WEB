using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination
{
    public class PagedList<T> : List<T>
    {
        public PagedList() { }

        public PagedList(IEnumerable<T> source, int page = 0, int pageSize = 1) : base(source)
        {
            _page = page;
            _pageSize = pageSize;
        }

        private int _page;
        private int _pageSize = 1;

        public int Page
        {
            get { return _page; }
            set { _page = value >= 0 ? value : 0; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > 0 ? value : 1; }
        }

        public int TotalItemCount { get; set; }

        public int TotalPageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;

                int mod = TotalItemCount % PageSize;
                int div = TotalItemCount / PageSize;

                if (mod != 0) div++;

                return div;
            }
        }
    }
}
