using System.Collections.Generic;

namespace VHKPlayer.Infrastructure
{
    public static class Extensions
    {
        public static int ToInteger(this string s)
        {
            int n;
            int.TryParse(s, out n);
            return n;
        }

        public static bool ToBool(this string s)
        {
            bool n;
            bool.TryParse(s, out n);
            return n;
        }

        public static bool IsEmpty<T>(this Queue<T> queue)
        {
            return queue.Count == 0;
        }

        public static void SetQueue<T>(this Queue<T> queue, IEnumerable<T> collection)
        {
            queue.Clear();

            foreach (var item in collection)
            {
                queue.Enqueue(item);
            }
        }
    }
}