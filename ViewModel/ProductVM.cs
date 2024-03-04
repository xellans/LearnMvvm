using System.Windows.Input;
using WpfCore;
using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace ViewModel
{
    public class ProductVM: ViewModelBase, IProductVM
    {
        public ProductVM(IProductModel Repository)
        {
            this.Repository = Repository;
            ProductDataList = Repository.ProductCollections;
            if(ProductDataList.Count() > 0)
            Selected = ProductDataList[0];
        }

        public IReadOnlyObservableCollection<IProduct> ProductDataList { get => Get<IReadOnlyObservableCollection<IProduct>>(); set => Set(value); }


        IProductModel Repository { get; set; }

        public IProduct Selected { get => Get<IProduct>(); set => Set(value); }
        #region Сохранение изменений

        public ICommand SaveEdit => GetCommand(SaveEditExcute);
        private void SaveEditExcute()
        {
            if (Selected != null)
                Repository.Product.Update(Selected);
        }
        #endregion

        #region Удаление записей

        public ICommand Delete => GetCommand(_DeleteExcute);
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

        public ICommand Add => GetCommand(AddExcute);
        private void AddExcute()
        {
            if (Selected != null)
                Repository.Product.Add(Selected);

        }
        #endregion



        #region Заполнение Selected случаныйыми данными из коллекции

        public ICommand RandomData => GetCommand(RandomDataExcute);
        private void RandomDataExcute()
        {
            if (Selected != null)
            {
                Selected.Name = ProductDataList[Random.Shared.Next(ProductDataList.Count())].Name;
                Selected.Description = ProductDataList[Random.Shared.Next(ProductDataList.Count())].Description;
            }

        }
        #endregion
    }
}
