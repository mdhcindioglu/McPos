using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace McPos
{
    public static class StringExtensions
    {
        public static bool HasValue(this string str) => !str.IsNullOrWhiteSpace() && !str.IsNullOrEmpty();
        public static bool IsNullOrWhiteSpace(this string str) => String.IsNullOrWhiteSpace(str);
        public static bool IsNullOrEmpty(this string str) => String.IsNullOrEmpty(str);

        public static string GetDisplayName<TItem>(this string propertyName)
        {
            MemberInfo myProp = typeof(TItem).GetProperty(propertyName) as MemberInfo;
            var dd = myProp.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            return dd?.Name ?? "";
        }
    }
}
