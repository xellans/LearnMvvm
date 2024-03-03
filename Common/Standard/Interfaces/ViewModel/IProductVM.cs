using Common.Standard.Interfaces.Model;

namespace Common.Standard.Interfaces.ViewModel
{
    public interface IProductVM
    {
        IReadOnlyObservableCollection<IProduct> ProductDataList { get;}

    }
}
