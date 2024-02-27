using Common.Standard.Interfaces.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfCore;

namespace ViewModel
{
    public class ProductVM: ViewModelBase
    {
        public ProductVM()
        {
            //Product = new();
            ProductDataList = new();
            Load();
            Selected = new ProductData();
        }
        #region Заполнение данными
        private void Load()
        {
            //foreach (var res in Product.ProductCollections)
            //    ProductDataList.Add(new ProductData() { Id = res.Id, Name = res.Name, Description = res.Description });
        }
        #endregion

         public ObservableCollection<ProductData> ProductDataList { get => Get<ObservableCollection<ProductData>>(); set => Set(value); }

        private readonly IRepository<IProduct> products;

        public ProductData Selected { get => Get<ProductData>(); set => Set(value); }
        #region Сохранение изменений

        private ICommand _SaveEdit;
        public ICommand SaveEdit => _SaveEdit ??  new RelayCommand(SaveEditExcute);
        private void SaveEditExcute()
        {
            //if (Selected != null)
            //    Product.Command.Update(Selected, Selected.Id);
        }
        #endregion

        #region Удаление записей

        private ICommand _Delete;
        public ICommand Delete => _Delete ?? new RelayCommand(_DeleteExcute);
        private void _DeleteExcute()
        {
            if (Selected != null)
            {
                //Product.Command.Remove(Selected.Id);
                ProductDataList.Remove(Selected);
                Selected = ProductDataList.LastOrDefault();
            }
        }
        #endregion

        #region Добавление новых записей

        private ICommand _Add;
        public ICommand Add => _Add ?? new RelayCommand(AddExcute);
        private void AddExcute()
        {
            //var newProduct = Product.Add(Selected);
            //ProductData newProductData = new();
            //Mapper.CopyProperties(newProduct, newProductData);
            //ProductDataList.Add(newProductData);
        }
        #endregion
    }
    public class ProductData: ViewModelBase, IProduct
    {
        public int Id { get => Get<int>(); set => Set(value); }

        public string Name { get => Get<string>(); set => Set(value); }

        public string Description { get => Get<string>(); set => Set(value); }
    }
}
