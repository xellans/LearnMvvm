using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using System.Windows.Input;
using WpfCore;

namespace ViewModel
{
    public class ProductVM : ViewModelBase, IProductVM
    {
        public ProductVM(IRepository<IProduct> products)
        {
            //Product = new();
            this.products = products;
            ProductDataList = products.ToObservableCollections();
            products.Load();
            if (ProductDataList.Count > 0)
                Selected = ProductDataList[0];
        }
        //#region Заполнение данными
        //private void Load()
        //{
        //    foreach (var res in Product.ProductCollections)
        //        ProductDataList.Add(new ProductData() { Id = res.Id, Name = res.Name, Description = res.Description });
        //}
        //#endregion

        public IReadOnlyObservableCollection<IProduct> ProductDataList { get; }

        private readonly IRepository<IProduct> products;

        public IProduct? Selected { get => Get<IProduct>(); set => Set(value); }
        #region Сохранение изменений

        //private ICommand _SaveEdit;
        public ICommand SaveEdit => GetCommand(SaveEditExcute);
        private void SaveEditExcute()
        {
            //if (Selected != null)
            //    Product.Command.Update(Selected, Selected.Id);
        }
        #endregion

        #region Удаление записей

        //private ICommand _Delete;
        public ICommand Delete => GetCommand(_DeleteExcute);

        private void _DeleteExcute()
        {
            if (Selected != null)
            {
                int index = ProductDataList.TakeWhile(pr => pr == Selected).Count();
                //Product.Command.Remove(Selected.Id);
                //ProductDataList.Remove(Selected);
                products.Remove(Selected);
                if (ProductDataList.Count > 0)
                {
                    if (index >= ProductDataList.Count)
                    {
                        index = ProductDataList.Count - 1;
                    }
                    Selected = ProductDataList[index];
                }
                Selected = null;
            }
        }
        #endregion

        #region Добавление новых записей

        //private ICommand _Add;
        public ICommand Add => GetCommand(AddExcute);
        private void AddExcute()
        {
            //var newProduct = Product.Add(Selected);
            //ProductData newProductData = new();
            //Mapper.CopyProperties(newProduct, newProductData);
            //ProductDataList.Add(newProductData);
        }
        #endregion
    }
    //public class ProductData: ViewModelBase, IProduct
    //{
    //    public long Id { get => Get<long>(); set => Set(value); }

    //    public string Name { get => Get<string>(); set => Set(value); }

    //    public string Description { get => Get<string>(); set => Set(value); }
    //}
}
