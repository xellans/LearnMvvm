using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using System.Windows.Input;
using WpfCore;

namespace LearnMvvm.Model.ViewModel
{
    public class ProductVM : ViewModelBase, IProductVM
    {
        public ProductVM(IRepository<IProduct> products)
        {
            this.products = products;
            ProductDataList = products.ToObservableCollections();
            if (ProductDataList.Count > 0)
            {
                Selected = ProductDataList[0];
            }
        }

        public IReadOnlyObservableCollection<IProduct> ProductDataList { get; }

        private readonly IRepository<IProduct> products;

        public IProduct? Selected { get => Get<IProduct>(); set => Set(value); }
        #region Сохранение изменений

        public ICommand SaveEdit => GetCommand(SaveEditExcute);
        private void SaveEditExcute()
        {
            if (Selected != null)
                products.Update(Selected);
        }
        #endregion

        #region Удаление записей
        public ICommand Delete => GetCommand(DeleteExcute);
        private void DeleteExcute()
        {
            if (Selected != null)
            {
                int index = ProductDataList.IndexOf(Selected);
                products.Remove(Selected);
                ProductDataList.Remove(Selected);
                if (index == ProductDataList.Count)
                    index--;
                if (index >= 0)
                    Selected = ProductDataList[index];
            }
        }
        #endregion

        #region Добавление новых записей

        public ICommand Add => GetCommand(AddExcute);
        private void AddExcute()
        {
            if (Selected != null)
                products.Add(Selected);
        }
        #endregion
    }
}
