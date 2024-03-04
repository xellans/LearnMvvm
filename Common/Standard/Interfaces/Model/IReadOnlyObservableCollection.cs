using System;
using System.Collections;
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
    {
        new int Count { get; }
        new T this[int index] { get; }
    }
}
