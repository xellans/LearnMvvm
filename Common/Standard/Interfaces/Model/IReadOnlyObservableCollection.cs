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
        ICollection,
        IList,
        INotifyCollectionChanged,
        INotifyPropertyChanged
    { }

    public class ReadOnlyObservableList<T, Tsource> : IReadOnlyObservableCollection<T>
        where T : class
        where Tsource : T
    {
        private readonly ObservableCollection<Tsource> obslist;
        private readonly IList list;
        private readonly ICollection collection;
        private readonly INotifyPropertyChanged notifyyPropertyChanged;
        public ReadOnlyObservableList(ObservableCollection<Tsource> list)
        {
            ArgumentNullException.ThrowIfNull(list);
            obslist = list;
            this.list = list;
            collection = list;
            notifyyPropertyChanged = list;
        }

        public T this[int index] => obslist[index];
        object? IList.this[int index] { get => obslist[index]; set => throw new NotImplementedException(); }

        public int Count => obslist.Count;
        public bool IsFixedSize => list.IsFixedSize;
        public bool IsReadOnly => list.IsReadOnly;
        public bool IsSynchronized => list.IsSynchronized;
        public object SyncRoot => list.SyncRoot;

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

        public void CopyTo(Array array, int index) => list.CopyTo(array, index);

        public IEnumerator<T> GetEnumerator()
        {
            foreach (Tsource item in obslist)
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
