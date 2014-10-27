using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zargess.VHKPlayer.Collections {
    public static class CollectionMethods {
        public static void AddOnUI<T>(this ICollection<T> collection, T item) {
            OperationOnCollection(collection.Add, item);
        }

        public static void RemoveOnUI<T>(this ICollection<T> collection, T item) {
            Func<T, bool> operation = collection.Remove;
            Application.Current.Dispatcher.BeginInvoke(operation, item);
        }

        public static void ClearOnUI<T>(this ICollection<T> collection) {
            foreach (var item in collection) {
                RemoveOnUI(collection, item);
            }
        }

        private static void OperationOnCollection<T>(Action<T> operation, T item) {
            Application.Current.Dispatcher.BeginInvoke(operation, item);
        }
    }
}
