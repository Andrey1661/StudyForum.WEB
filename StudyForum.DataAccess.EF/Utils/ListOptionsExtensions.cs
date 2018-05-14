using System.Linq;
using Pagination;
using StudyForum.DataAccess.Core.Enviroment;

namespace StudyForum.DataAccess.Utils
{
    internal static class ListOptionsExtensions
    {
        public static IQueryable<T> TakeFrom<T>(this ListOptions listOptions, IQueryable<T> query)
        {
            return listOptions != null ? query.Skip(listOptions.Offset).Take(listOptions.PageSize) : query;
        }

        public static PagedList<T> CreatePagedList<T>(this ListOptions listOptions, int totalItemCount)
        {
            var list = new PagedList<T>
            {
                TotalItemCount = totalItemCount
            };

            if (listOptions != null)
            {
                list.Page = listOptions.Page;
                list.PageSize = listOptions.PageSize;
            }

            return list;
        }
    }
}
