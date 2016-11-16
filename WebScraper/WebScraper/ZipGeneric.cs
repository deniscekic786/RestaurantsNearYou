using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{

    /// <summary>
    /// Custom linq zip method to add 8 lists to be enumerated
    /// This is just specific to how many list of nodes I needed to quickly iterate
    /// unique to yelp.com
    /// </summary>
    public static class ZipGeneric
    {
        public static IEnumerable<TResult> Zip8<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSvnth, TEigth, TResult>(
                             this IEnumerable<TFirst> first,
                             IEnumerable<TSecond> second,
                             IEnumerable<TThird> third,
                             IEnumerable<TFourth> fourth,
                             IEnumerable<TFifth> fifth,
                             IEnumerable<TSixth> sixth,
                                IEnumerable<TSvnth> seventh,
                                  IEnumerable<TEigth> eigth,
                       Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSvnth, TEigth, TResult> resultSelector)
        {
            using (IEnumerator<TFirst> iterator1 = first.GetEnumerator())
            using (IEnumerator<TSecond> iterator2 = second.GetEnumerator())
            using (IEnumerator<TThird> iterator3 = third.GetEnumerator())
            using (IEnumerator<TFourth> iterator4 = fourth.GetEnumerator())
            using (IEnumerator<TFifth> iterator5 = fifth.GetEnumerator())
            using (IEnumerator<TSixth> iterator6 = sixth.GetEnumerator())
            using (IEnumerator<TSvnth> iterator7 = seventh.GetEnumerator())
            using (IEnumerator<TEigth> iterator8 = eigth.GetEnumerator())
            {
                while (iterator1.MoveNext() && iterator2.MoveNext() && iterator3.MoveNext() && iterator4.MoveNext() && iterator5.MoveNext() && iterator6.MoveNext() && iterator7.MoveNext() && iterator8.MoveNext())
                {
                    yield return resultSelector(iterator1.Current, iterator2.Current, iterator3.Current, iterator4.Current, iterator5.Current, iterator6.Current, iterator7.Current, iterator8.Current);
                }
            }
        }

    }
}
