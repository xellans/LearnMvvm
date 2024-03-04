using Common.EntityFrameworkCore;
using Common.Standard.Interfaces.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataBase
{
    internal static class RepositoryMemories
    {
        public static Product ItToT(IProduct iproduct, DbContext context)
        {
            var product = context.CreateProxy<Product>();
            product.Description = iproduct.Description;
            product.Name = iproduct.Name;
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
        public static Person? ItToT(IPerson iperson, DbContext context)
        {
            var person = context.CreateProxy<Person>();
            person.Name = iperson.Name;
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

        public static User ItToT(IUser iuser, DbContext context)
        {
            var user = context.CreateProxy<User>();
            user.IsAuthorized = iuser.IsAuthorized;
            user.Name = iuser.Name;
            return user;
        }

        public static bool EqualsValues(IUser iuser, User user)
        {
            return user.Name == iuser.Name;
        }
        public static User Convert(IUser iuser)
        {
            User user = new()
            {
                Id = iuser.Id,
                Name = iuser.Name,
                IsAuthorized = iuser.IsAuthorized
            };
            return user;
        }
    }

    public class ContextRepositories
    {
        private Context context {  get; set; }
        public ContextRepositories(Context context)
        {
            this.context = context;
        }
        public IRepository<IProduct> Products()
        {
            Repository<IProduct, Product> products = new(context,
                                                         RepositoryMemories.ItToT,
                                                         new Exception("Товара с таким Id нет."),
                                                         RepositoryMemories.EqualsValues,
                                                         new Exception("Товар с таким Id имеет другие свойства."),
                                                         RepositoryMemories.Convert);
            return products;
        }
        public IRepository<IPerson> Person()
        {
            Repository<IPerson, Person> people = new(context,
                                                     RepositoryMemories.ItToT,
                                                     new Exception("Человека с таким Id нет."),
                                                     RepositoryMemories.EqualsValues,
                                                     new Exception("Человек с таким Id имеет другие свойства."),
                                                     RepositoryMemories.Convert);
            return people;
        }

        public IRepository<IUser> User()
        {
            Repository<IUser, User> user = new(context,
                                                     RepositoryMemories.ItToT,
                                                     new Exception("Человека с таким Id нет."),
                                                     RepositoryMemories.EqualsValues,
                                                     new Exception("Человек с таким Id имеет другие свойства."),
                                                     RepositoryMemories.Convert);
            return user;
        }
    }
}