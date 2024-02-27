using Common.Standard.Interfaces.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataBase
{
    public class ProdustsRepository : IRepository<IProduct>
    {
        internal readonly Context context;

        public ProdustsRepository(Context context)
        {
            this.context = context;
        }

        public IProduct? Add(IProduct t)
        {
            if (t is not Product product)
            {
                product = new()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                };
            }
            var result = context.Products.Add(product);
            context.SaveChanges();
            return result.Entity;
        }

        public bool Any(Func<IProduct, bool> predicate)
        {
            return context.Products.Any(predicate);
        }

        //public IProduct? Clone(IProduct t)
        //{
        //    throw new NotImplementedException();
        //}

        public IProduct? FirstOrDefault()
        {
            return context.Products.FirstOrDefault();
        }

        public IProduct? FirstOrDefault(Expression<Func<IProduct, bool>> expression)
        {
            return context.Products.FirstOrDefault(expression);
        }

        public void Load()
        {
            context.Products.Load();
        }

        public void Remove(IProduct t)
        {
            var product = context.Products.Find(t.Id);
            if (product is null)
            {
                throw new Exception("Товара с таким Id нет.");
            }
            if (product.Name != t.Name || product.Description != t.Description)
            {
                throw new Exception("Товар с таким Id имеет другие свойства.");
            }
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void Remove(int Id)
        {
            var product = context.Products.Find(Id);
            if (product is null)
            {
                throw new Exception("Товара с таким Id нет.");
            }
            context.Products.Remove(product);
            context.SaveChanges();
        }

        private IReadOnlyObservableCollection<IProduct>? products;
        public IReadOnlyObservableCollection<IProduct> ToObservableCollections()
        {
            if (products is null)
            {
                var list = context.Products.Local.ToObservableCollection();
                products = new ReadOnlyObservableList<IProduct, Product>(list);
            }
            return products;
        }

        public IProduct Update(IProduct t)
        {
            var product = context.Products.Find(t.Id);
            if (product is null)
            {
                throw new Exception("Товара с таким Id нет.");
            }

            product = new()
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description
            };

            var result = context.Products.Update(product);
            context.SaveChanges();
            return result.Entity;
        }

        public IProduct Update(object NewValue, int Id)
        {
            var product = context.Products.Find(Id);
            if (product is null)
            {
                throw new Exception("Товара с таким Id нет.");
            }

            context.Entry(product).CurrentValues.SetValues(NewValue);
            context.SaveChanges();

            return product;
        }

        public IQueryable<IProduct> Where(Expression<Func<IProduct, bool>> expression)
        {
            return context.Products.Where(expression);
        }
    }
}