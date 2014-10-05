using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Zargess.VHKPlayer.Collections {
    public class ObservableSafeCollection<T> : ObservableCollection<T> {
        // Override the event so this class can access it
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableSafeCollection() : base() { }
        public ObservableSafeCollection(IEnumerable<T> collection) : base(collection) { }
        public ObservableSafeCollection(List<T> collection) : base(collection) { }

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
    }
}
