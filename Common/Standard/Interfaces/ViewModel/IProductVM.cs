
using Common.Standard.Interfaces.Model;
using System.Windows.Input;

namespace Common.Standard.Interfaces.ViewModel
{
    public interface IProductVM
    {
        ICommand Add { get; }
        ICommand Delete { get; }
        IReadOnlyObservableCollection<IProduct> ProductDataList { get; }
        ICommand SaveEdit { get; }
        IProduct? Selected { get; set; }
    }

}
