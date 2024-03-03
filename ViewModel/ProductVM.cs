using System.Windows.Input;
using WpfCore;
using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;

namespace ViewModel
{
    public class ProductVM: ViewModelBase, IProductVM
    {
        public ProductVM(IProductModel repository)
        {
            this.repository = repository;
            ProductDataList = repository.ProductCollections;
            Selected = repository.Product.NewT();
        }
        public IReadOnlyObservableCollection<IProduct> ProductDataList { get => Get<IReadOnlyObservableCollection<IProduct>>(); set => Set(value); }


        IProductModel repository { get; set; }

        public IProduct Selected { get => Get<IProduct>(); set => Set(value); }
        #region Сохранение изменений

        private ICommand _SaveEdit;
        public ICommand SaveEdit => _SaveEdit ??  new RelayCommand(SaveEditExcute);
        private void SaveEditExcute()
        {
            if (Selected != null)
                repository.Product.Update(Selected);
        }
        #endregion

        #region Удаление записей

        private ICommand _Delete;
        public ICommand Delete => _Delete ?? new RelayCommand(_DeleteExcute);
        private void _DeleteExcute()
        {
            if (Selected != null)
            {
                int index  = ProductDataList.IndexOf(Selected);
                repository.Product.Remove(Selected);
                //ProductDataList.Remove(Selected);
                if (index == ProductDataList.Count())
                    index--;
                if(index >= 0)
                Selected = ProductDataList[index];
            }
        }
        #endregion

        #region Добавление новых записей

        private ICommand _Add;
        public ICommand Add => _Add ?? new RelayCommand(AddExcute);
        private void AddExcute()
        {
            if (Selected != null)
                repository.Product.Add(Selected);
        }
        #endregion
    }
}
