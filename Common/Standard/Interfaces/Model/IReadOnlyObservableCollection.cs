using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Common.Standard.Interfaces.Model
{
    public interface IReadOnlyObservableCollection<T> :
        IEnumerable<T>,
        IEnumerable,
        IReadOnlyCollection<T>,
        IReadOnlyList<T>,
        //ICollection,
        //IList,
        INotifyCollectionChanged,
        INotifyPropertyChanged
    {
        //new int Count { get; }
        //new T this[int index] { get; }

    }


    public class ReadOnlyObservableList<T> : ReadOnlyObservableCollection<T>, IReadOnlyObservableCollection<T>
    {
        public ReadOnlyObservableList(ObservableCollection<T> list) : base(list)
        {
        }

        public static ReadOnlyObservableList<T> Empty = new ReadOnlyObservableList<T> (new ObservableCollection<T>());
    }
    public class ReadOnlyObservableList<T, TSource> : IReadOnlyObservableCollection<T>
        where TSource : T
    {
        private readonly ObservableCollection<TSource> obslist;
        private readonly IList list;
        private readonly ICollection collection;
        private readonly INotifyPropertyChanged notifyyPropertyChanged;
        public ReadOnlyObservableList(ObservableCollection<TSource> list)
        {
            ArgumentNullException.ThrowIfNull(list);
            obslist = list;
            this.list = list;
            collection = list;
            notifyyPropertyChanged = list;
        }

        public T this[int index] => obslist[index];
        //object? IList.this[int index] { get => obslist[index]; set => throw new NotImplementedException(); }

        public int Count => obslist.Count;
        public bool IsFixedSize => true; //list.IsFixedSize;
        public bool IsReadOnly => true;//list.IsReadOnly;
        public bool IsSynchronized => collection.IsSynchronized;
        public object SyncRoot => collection.SyncRoot;

        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add => obslist.CollectionChanged += value;
            remove => obslist.CollectionChanged -= value;
        }

        public event PropertyChangedEventHandler? PropertyChanged
        {
            add => notifyyPropertyChanged.PropertyChanged += value;
            remove => notifyyPropertyChanged.PropertyChanged -= value;
        }

        public int Add(object? value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object? value) => list.Contains(value);

        public void CopyTo(Array array, int index) => collection.CopyTo(array, index);

        public IEnumerator<T> GetEnumerator()
        {
            foreach (TSource item in obslist)
            {
                yield return item;
            }
        }

        public int IndexOf(object? value) => list.IndexOf(value);

        public void Insert(int index, object? value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object? value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
