using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Common.Standard.Interfaces.Model
{
    public class ObservableCollection<TTarget, TSource> : ObservableCollection<TTarget>
    {
        private readonly ObservableCollection<TSource> sources;
        private readonly Func<TSource, TTarget> toTarget;
        private readonly Func<TTarget, TSource> toSource;

        private static IEnumerable<TTarget> GetEnumerable(IEnumerable<TSource> sources, Func<TSource, TTarget> toTarget)
        {
            foreach (TSource source in sources)
            {
                yield return toTarget(source);
            }
        }

        public ObservableCollection(ObservableCollection<TSource> sources, Func<TSource, TTarget> toTarget, Func<TTarget, TSource> toSource, Action<Action<NotifyCollectionChangedEventArgs>, NotifyCollectionChangedEventArgs> сollectionChangedMarshaling, Action<Action<PropertyChangedEventArgs>, PropertyChangedEventArgs> propertyChangedMarshaling)
            : base(GetEnumerable(sources, toTarget))
        {
            this.sources = sources;
            this.toTarget = toTarget;
            this.toSource = toSource;
            sources.CollectionChanged += OnCollectionChanged;
            this.сollectionChangedMarshaling = сollectionChangedMarshaling ?? new((action, args) => action(args));
            this.propertyChangedMarshaling = propertyChangedMarshaling ?? new((action, args) => action(args));
        }

        private bool isToTarget = false;
        private bool isToSource = false;
        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (isToSource)
                return;
            isToTarget = true;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        foreach (TSource item in e.NewItems!)
                            InsertItem(Count - 1, toTarget(item));
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
                        int i = 0, index = e.NewStartingIndex;
                        foreach (TSource item in e.NewItems!)
                        {
                            if (i < e.NewItems!.Count)
                                break;
                            SetItem(index, toTarget(item));
                            i++; index++;
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
            isToTarget = false;
        }

        protected override void ClearItems()
        {
            base.ClearItems();

            if (isToTarget)
                return;
            isToSource = true;
            sources.Clear();
            isToSource = false; ;
        }

        protected override void InsertItem(int index, TTarget item)
        {
            base.InsertItem(index, item);

            if (isToTarget)
                return;
            isToSource = true;
            sources.Insert(index, toSource(item));
            isToSource = false; ;
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);

            if (isToTarget)
                return;
            isToSource = true;
            sources.Move(oldIndex, newIndex);
            isToSource = false; ;
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);

            if (isToTarget)
                return;
            isToSource = true;
            sources.RemoveAt(index);
            isToSource = false; ;
        }

        protected override void SetItem(int index, TTarget item)
        {
            base.SetItem(index, item);

            if (isToTarget)
                return;
            isToSource = true;
            sources[index] = toSource(item);
            isToSource = false; ;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            сollectionChangedMarshaling(base.OnCollectionChanged, e);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            propertyChangedMarshaling(base.OnPropertyChanged, e);
        }

        private readonly Action<Action<NotifyCollectionChangedEventArgs>, NotifyCollectionChangedEventArgs> сollectionChangedMarshaling;
        private readonly Action<Action<PropertyChangedEventArgs>, PropertyChangedEventArgs> propertyChangedMarshaling;
    }
}
