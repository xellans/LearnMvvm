using DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Inerfaces;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls.Primitives;
using System.Collections.Specialized;
using WpfCore;
using System.Security.Cryptography;

namespace ViewModel
{
    public class ProductVM: ViewModelBase
    {
        public ProductVM()
        {
            CommandProduct = new();
            ProductDataList = new();
            Load();
            Selected = new ProductData();
        }
        #region Заполнение данными
        private void Load()
        {
            var data = CommandProduct.GetProductCollection();

            if (data.Count == 0)
                CommandProduct.CreateProduct();

            foreach (var res in CommandProduct.GetProductCollection())
                ProductDataList.Add(new ProductData() { Id = res.Id, Name = res.Name, Description = res.Description });
        }
        #endregion

        public ObservableCollection<ProductData> ProductDataList { get => Get<ObservableCollection<ProductData>>(); set => Set(value); }
      
        private Command<Product> CommandProduct { get; set; }

        public ProductData Selected { get => Get<ProductData>(); set => Set(value); }
        #region Сохранение изменений

        private ICommand _SaveEdit;
        public ICommand SaveEdit => _SaveEdit ??  new RelayCommand(SaveEditExcute);
        private void SaveEditExcute()
        {
            if (Selected != null)
                CommandProduct.Update(Selected, Selected.Id);
        }
        #endregion

        #region Удаление записей

        private ICommand _Delete;
        public ICommand Delete => _Delete ?? new RelayCommand(_DeleteExcute);
        private void _DeleteExcute()
        {
            if (Selected != null)
            {
                CommandProduct.Remove(Selected.Id);
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
            Product product = new Product()
            {
                Name = Selected.Name,
                Description = Selected.Description
            };
            var temp = CommandProduct.Add(product);
            var newProductData = new ProductData();
            Mapper.CopyProperties(temp, newProductData);
            ProductDataList.Add(newProductData);
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
