using DataBase.Realisation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    public interface IProductRepository
    {
        Command<Product> Command { get; set; }
        ReadOnlyObservableCollection<Product> ProductCollections { get; }
        void CreateProduct();
    }
}
