using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Enviroment
{
    public class ListOptions
    {
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
    }
}
