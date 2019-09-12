using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoPandas.Pandamo.Utilities
{
    internal static class ListHelpers
    {
        /// <summary>
        ///     Swaps rows and columns in a list of lists. 
        ///     If there are some rows that are shorter than others,
        ///     null values are inserted as place holders in the resultant 
        ///     array such that it is always rectangular.
        /// </summary>
        /// <param name="lists">A list of lists to be transposed.</param>
        /// <returns name="lists">A list of transposed lists.</returns>
        /// <search>transpose,flip matrix,matrix,swap,rows,columns</search>

        internal static IList Transpose(IList lists)
        {
            if (lists.Count == 0 || !lists.Cast<object>().Any(x => x is IList))
                return lists;

            IEnumerable<IList> ilists = lists.Cast<IList>();
            int maxLength = ilists.Max(subList => subList.Count);
            List<ArrayList> transposedList =
                Enumerable.Range(0, maxLength).Select(i => new ArrayList()).ToList();

            foreach (IList sublist in ilists)
            {
                for (int i = 0; i < transposedList.Count; i++)
                {
                    transposedList[i].Add(i < sublist.Count ? sublist[i] : null);
                }
            }

            return transposedList;
        }
    }
}
