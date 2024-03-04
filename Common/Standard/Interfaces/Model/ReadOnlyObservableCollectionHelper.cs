using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Common.Standard.Interfaces.Model
{
    public static class ReadOnlyObservableCollectionHelper
    {
        public static ReadOnlyObservableCollection<T> GetReadOnlyObservableCollection<T>(this ObservableCollection<T> sources)
            => new(sources);

        public static ReadOnlyObservableCollection<TTarget> GetReadOnlyObservableCollection<TTarget, TSource>(this ObservableCollection<TSource> sources)
            where TSource : TTarget
            => new(new ObservableCollection<TTarget, TSource>(sources));

        private class ObservableCollection<TTarget, TSource> : ObservableCollection<TTarget>
            where TSource : TTarget
        {
            private readonly ObservableCollection<TSource> sources;
            private static IEnumerable<TTarget> GetEnumerable(IEnumerable<TSource> sources)
            {
                foreach (TSource source in sources)
                {
                    yield return source;
                }
            }

            public ObservableCollection(ObservableCollection<TSource> sources)
                : base(GetEnumerable(sources))
            {
                this.sources = sources;
                sources.CollectionChanged += OnCollectionChanged;
            }

            private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        {
                            foreach (TSource item in e.NewItems!)
                                InsertItem(Count - 1, item);
                            break;
                        }
                    case NotifyCollectionChangedAction.Remove:
                        {
                            for (int index = e.OldStartingIndex + e.OldItems!.Count - 1; index < e.OldStartingIndex; index--)
                                RemoveItem(index);
                            break;
                        }
                    case NotifyCollectionChangedAction.Reset:
                        {
                            ClearItems();
                            break;
                        }
                    case NotifyCollectionChangedAction.Replace:
                        {
                            for (int i = 0, index = e.NewStartingIndex; i < e.NewItems!.Count; i++, index++)
                            {
                                SetItem(index, (TSource)e.NewItems[i]!);
                            }
                            break;
                        }
                    case NotifyCollectionChangedAction.Move:
                        {
                            if (e.NewStartingIndex < e.OldStartingIndex)
                            {
                                for (int i = 0, newIndex = e.NewStartingIndex, oldIndex = e.OldStartingIndex;
                                    i < e.NewItems!.Count; i++, newIndex++, oldIndex++)
                                {
                                    MoveItem(oldIndex, newIndex);
                                }
                            }
                            else
                            {
                                for (int i = e.NewItems!.Count - 1,
                                         newIndex = e.NewStartingIndex + i,
                                         oldIndex = e.OldStartingIndex + i;
                                    i >= 0; i--, newIndex--, oldIndex--)
                                {
                                    MoveItem(oldIndex, newIndex);
                                }
                            }
                            break;
                        }
                }
            }
        }
    }
}
