using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination
{
    public class ListOptions
    {
        private int _page;
        private int _pageSize = 1;

        public ListOptions()
        {

        }

        public ListOptions(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page
        {
            get { return _page; }
            set { _page = value >= 0 ? value : 0; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value >= 0 ? value : 0; }
        }

        public int Offset => Page * PageSize;
    }
}
