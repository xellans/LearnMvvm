using System.Collections.ObjectModel;

namespace Common.Standard.Interfaces.Model
{
    public class ReadOnlyObservableList<T> : ReadOnlyObservableCollection<T>, IReadOnlyObservableCollection<T>
    {
        public ReadOnlyObservableList(ObservableCollection<T> list) : base(list)
        {
        }

        public static ReadOnlyObservableList<T> Empty = new ReadOnlyObservableList<T> (new ObservableCollection<T>());
    }
}
