
using Common.Standard.Interfaces.Model;
using WpfCore;

namespace Common.Standard.Interfaces.ViewModel
{
    public interface IPersonVM
    {
        IReadOnlyObservableCollection<IPerson> PersonList { get; }
        RelayCommand Refresh { get; }
    }
}
