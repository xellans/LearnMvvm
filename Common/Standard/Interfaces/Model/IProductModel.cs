using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Standard.Interfaces.Model
{
    public interface IProductModel
    {
        public IRepository<IProduct> Product { get; set; }
        public IReadOnlyObservableCollection<IProduct> ProductCollections { get; set; }
        void CreateProduct();
    }
}
