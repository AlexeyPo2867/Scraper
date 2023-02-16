using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBScraping_c
{
   public static class Extensions
    {
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            return items.Contains(item);
        }
    }
}
