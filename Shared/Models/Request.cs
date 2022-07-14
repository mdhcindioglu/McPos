using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McPos.Shared.Models
{
    public class Request
    {
        public string Search { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = "Id desc";
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
        public int CurPage { get; set; } = 1;
    }
}
