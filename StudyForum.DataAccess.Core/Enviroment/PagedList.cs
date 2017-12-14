using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Enviroment
{
    public class PagedList<T> : List<T>
    {
        public PagedList() { }

        public PagedList(IEnumerable<T> source) : base(source) { }

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
            set { _pageSize = value >= 0 ? value : 0; }
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
