﻿using System.Collections;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace Common.WpfCore
{
    public static partial class CollectionBindingHelper
    {
        public static void EnableCollectionSynchronization(this ICollection collection)
        {
            if (collection is null)
                return;
            if (SynchronizationContext.Current is DispatcherSynchronizationContext context)
            {
                BindingOperations.EnableCollectionSynchronization(collection, collection.SyncRoot);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(collection.EnableCollectionSynchronization);
            }
        }
        public static void DisableCollectionSynchronization(this ICollection collection)
        {
            if (collection is null)
                return;
            if (SynchronizationContext.Current is DispatcherSynchronizationContext context)
            {
                BindingOperations.DisableCollectionSynchronization(collection);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(collection.DisableCollectionSynchronization);
            }
        }
    }
}
