using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace VHKPlayer.Collections {
    public class SortableCollection<T> : ObservableCollection<T> {
        // Override the event so this class can access it
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        public SortableCollection() : base() { }
        public SortableCollection(IEnumerable<T> collection) : base(collection) { }
        public SortableCollection(List<T> collection) : base(collection) { }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            // Be nice - use BlockReentrancy like MSDN said
            using (BlockReentrancy()) {
                var eventHandler = CollectionChanged;
                if (eventHandler == null) return;
                var delegates = eventHandler.GetInvocationList();
                // Walk thru invocation list
                foreach (var @delegate in delegates) {
                    var handler = (NotifyCollectionChangedEventHandler)@delegate;
                    var dispatcherObject = handler.Target as DispatcherObject;
                    // If the subscriber is a DispatcherObject and different thread
                    if (dispatcherObject != null && dispatcherObject.CheckAccess() == false)
                        // Invoke handler in the target dispatcher's thread
                        dispatcherObject.Dispatcher.Invoke(DispatcherPriority.DataBind,
                            handler, this, e);
                    else // Execute handler as is
                        handler(this, e);
                }
            }
        }

        #region Sorting

        /// <summary>
        /// Sorts the items of the collection in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        public void Sort<TKey>(Func<T, TKey> keySelector) {
            InternalSort(Items.OrderBy(keySelector));
        }

        /// <summary>
        /// Sorts the items of the collection in descending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        public void SortDescending<TKey>(Func<T, TKey> keySelector) {
            InternalSort(Items.OrderByDescending(keySelector));
        }

        /// <summary>
        /// Sorts the items of the collection in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        public void Sort<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer) {
            InternalSort(Items.OrderBy(keySelector, comparer));
        }

        /// <summary>
        /// Moves the items of the collection so that their orders are the same as those of the items provided.
        /// </summary>
        /// <param name="sortedItems">An <see cref="IEnumerable{T}"/> to provide item orders.</param>
        private void InternalSort(IEnumerable<T> sortedItems) {
            var sortedItemsList = sortedItems.ToList();

            foreach (var item in sortedItemsList) {
                Move(IndexOf(item), sortedItemsList.IndexOf(item));
            }
        }

        #endregion // Sorting
    }
}
