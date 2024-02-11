using Entity;
using Helpers;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DataBase.Repositories
{
    internal class PeopleRepository : IPeopleRepository
    {
        internal readonly UsersPeopleDb db;
        internal readonly string dataBaseNamePath;

        public PeopleRepository(UsersPeopleDb db, string dataBaseNamePath)
        {
            this.db = db;
            this.dataBaseNamePath = dataBaseNamePath;
        }

        private ReadOnlyObservableCollection<Person>? people;

        public ReadOnlyObservableCollection<Person> GetCollection()
        {
            return people ??= new ReadOnlyObservableCollection<Person>(db.People.Local.ToObservableCollection());
        }

        public Person Clone(Person t)
        {
            return new Person()
            {
                Id = t.Id,
                Name = t.Name,
                Age = t.Age
            };
        }

        public Person? Add(Person t)
        {
            if (t.Id != 0)
                throw new NotImplementedException();

            // Временный контекст бд, на одну транзакцию,
            // чтобы можно было откатить изменения.
            int changes; // Количество изменений
            using (UsersPeopleDb tempDb = new UsersPeopleDb(db.Database.ProviderName ?? string.Empty))
            {
                tempDb.People.Add(t);
                changes = tempDb.SaveChanges();
            }
            if (changes != 1)
            {
                // обработка ошибки
                return null;
            }
            else
            {
                // добавляем сущность в локаль.
                db.People.Add(t).State = EntityState.Unchanged;
            }
            return t;
        }

        public Person? Update(Person t)
        {
            if (t.Id == 0)
                throw new NotImplementedException();

            // Временный контекст бд, на одну транзакцию,
            // чтобы можно было откатить изменения.
            int changes; // Количество изменений
            using (UsersPeopleDb tempDb = new UsersPeopleDb(db.Database.ProviderName ?? string.Empty))
            {
                tempDb.People.Update(t);
                changes = tempDb.SaveChanges();
            }
            if (changes != 1)
            {
                // обработка ошибки
                return null;
            }
            else
            {
                // Заменяем сущности.
                db.People.Local.ToObservableCollection().ReplaceOrAdd(users => users.Id == t.Id, t);
                db.Entry(t).State = EntityState.Unchanged;
            }
            return t;
        }

        public bool Delete(int id)
        {
            if (id == 0)
                throw new NotImplementedException();

            // Временный контекст бд, на одну транзакцию,
            // чтобы можно было откатить изменения.
            int changes = 0; // Количество изменений
            using (UsersPeopleDb tempDb = new UsersPeopleDb(db.Database.ProviderName ?? string.Empty))
            {
                var old = tempDb.People.Find(id);
                if (old is not null)
                {
                    tempDb.People.Remove(old);
                    changes = tempDb.SaveChanges();
                }
            }
            if (changes != 1)
            {
                // обработка ошибки
                return false;
            }
            else
            {
                // удаляем сущность.
                db.People.Local.ToObservableCollection().RemoveFirst(users => users.Id == id);
            }
            return true;
        }
    }
}
