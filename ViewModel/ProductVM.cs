using DataBase.Command;
using InterfaceList;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.VmHellper;

namespace ViewModel
{
    public class ProductVM: BaseInpc
    {
        public ProductVM()
        {
            CommandProduct = new CommandProduct();
            PeopleList = new();
            var data = CommandProduct.OutputProduct();

            if (data.Count == 0)
                CommandProduct.CreateProduct();

            foreach (var res in CommandProduct.OutputProduct())
                PeopleList.Add(new ProductData() { Id = res.Id, Name = res.Name, Description = res.Description});

        }
        private ObservableCollection<ProductData> _PeopleList;

        public ObservableCollection<ProductData> PeopleList { get => _PeopleList; set => Set(ref _PeopleList, value); }
      
        private CommandProduct CommandProduct { get; set; }
    }
    public class ProductData: BaseInpc, IProduct
    {
        private int _Id;
        public int Id { get => _Id; set => Set(ref _Id, value); }

        private string _Name;
        public string Name { get => _Name; set => Set(ref _Name, value); }

        private string _Description;
        public string Description { get => _Description; set => Set(ref _Description, value); }
    }
}
