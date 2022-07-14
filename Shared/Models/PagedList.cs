using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McPos.Shared.Models
{
    public class PagedList<T>
    {
        public List<T>? Items { get; set; }
        public int SelectedCount { get; set; }
        public int AllItemCount { get; set; }
        public int Pages { get; set; }
        public bool IsLastPage { get; set; }
        public bool IsFirstPage { get; set; }
        public string? Details { get; set; }
        
        public int Pagination(int take, int curPage, string search)
        {
            Pages = Convert.ToInt32(Math.Ceiling((double)SelectedCount / (double)take));
            if (curPage > Pages) curPage = Pages == 0 ? 1 : Pages;
            IsFirstPage = curPage == 1;
            IsLastPage = curPage >= Pages;

            if (SelectedCount > 0)
            {
                Details = "View ";
                Details += $"({take * (curPage - 1) + 1}-";
                var toCount = take * curPage;
                toCount = toCount > SelectedCount ? SelectedCount : toCount;
                Details += $"{toCount}) from ";
                Details += $"{AllItemCount}";
                if (!string.IsNullOrEmpty(search))
                    Details += $"/{SelectedCount}";
            }
            else
                Details = "-";

            return (curPage - 1) * take;
        }
    }
}
