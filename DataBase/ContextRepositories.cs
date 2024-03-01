using Common.Standard.Interfaces.Model;

namespace DataBase
{
    internal static class RepositoryMemories
    {
        public static Product ItToT(IProduct iproduct)
        {
            if (iproduct is not Product product)
            {
                product = new()
                {
                    Id = iproduct.Id,
                    Name = iproduct.Name,
                    Description = iproduct.Description
                };
            }
            return product;
        }

        public static bool EqualsValues(IProduct iproduct, Product product)
        {
            return product.Name == iproduct.Name || product.Description == iproduct.Description;
        }

        public static Product Convert(IProduct iproduct)
        {
            Product product = new()
            {
                Id = iproduct.Id,
                Name = iproduct.Name,
                Description = iproduct.Description
            };
            return product;
        }
        public static Person ItToT(IPerson iperson)
        {
            if (iperson is not Person person)
            {
                person = new()
                {
                    Id = iperson.Id,
                    Name = iperson.Name
                };
            }
            return person;
        }

        public static bool EqualsValues(IPerson iperson, Person person)
        {
            return person.Name == iperson.Name;
        }

        public static Person Convert(IPerson iperson)
        {
            Person person = new()
            {
                Id = iperson.Id,
                Name = iperson.Name
            };
            return person;
        }
    }

    public class ContextRepositories
    {
        public ContextRepositories(Context context)
        {
            products = new(context,
                             RepositoryMemories.ItToT,
                             new Exception("Товара с таким Id нет."),
                             RepositoryMemories.EqualsValues,
                             new Exception("Товар с таким Id имеет другие свойства."),
                             RepositoryMemories.Convert);
            people = new(context,
                             RepositoryMemories.ItToT,
                             new Exception("Человека с таким Id нет."),
                             RepositoryMemories.EqualsValues,
                             new Exception("Человек с таким Id имеет другие свойства."),
                             RepositoryMemories.Convert);
        }

        private readonly Repository<IProduct, Product> products;
        private readonly Repository<IPerson, Person> people;

        public IRepository<IProduct> Products => products;
        public IRepository<IPerson> People => people;
    }
}