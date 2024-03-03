using Common.Standard.Interfaces.Model;

namespace Common.Standard.Interfaces.ViewModel
{
    public interface IPersonVM
    {
        public IReadOnlyObservableCollection<IPerson> PersonList { get;}
    }
}
