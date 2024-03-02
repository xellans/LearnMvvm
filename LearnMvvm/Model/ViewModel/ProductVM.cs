using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfCore;
using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using Repositories;
using Common.EntityFrameworkCore;

namespace LearnMvvm.Model.ViewModel
{
    public class ProductVM: ViewModelBase, IProductVM
    {
        public ProductVM()
        {
            Repository = new();
            ProductDataList = Repository.ProductCollections;
            Selected = Repository.Product.NewT();
        }
        public IReadOnlyObservableCollection<IProduct> ProductDataList { get => Get<IReadOnlyObservableCollection<IProduct>>(); set => Set(value); }


        private ProductModel Repository { get; set; }

        public IProduct Selected { get => Get<IProduct>(); set => Set(value); }
        #region Сохранение изменений

        private ICommand _SaveEdit;
        public ICommand SaveEdit => _SaveEdit ??  new RelayCommand(SaveEditExcute);
        private void SaveEditExcute()
        {
            if (Selected != null)
                Repository.Product.Update(Selected);
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
                Repository.Product.Remove(Selected);
                ProductDataList.Remove(Selected);
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
                Repository.Product.Add(Selected);
        }
        #endregion
    }
}
