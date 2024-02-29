using DataBase.Interfaces;
using DataBase.Realisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductModel
    {
        public IProductRepository Product { get; set; }
        public ProductModel()
        {
            Product = new ProductRepository();
        }
    }
}
