using Entity;
using Helpers;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.ObjectModel;
using System.IO;

namespace DataBase.Repositories
{
    internal class UsersRepository : IUsersRepository
    {
        internal readonly UsersPeopleDb db;
        internal readonly string dataBaseNamePath;

        public UsersRepository(UsersPeopleDb db, string dataBaseNamePath)
        {
            this.db = db;
            this.dataBaseNamePath = dataBaseNamePath;
        }

        private ReadOnlyObservableCollection<User>? users;

        public ReadOnlyObservableCollection<User> GetCollection()
        {
            return users ??= new ReadOnlyObservableCollection<User>(db.Users.Local.ToObservableCollection());
        }

        public User Clone(User t)
        {
            return new User()
            {
                Id = t.Id,
                Name = t.Name,
                IsAuthorized = t.IsAuthorized,
            };
        }

        public User? Add(User t)
        {
            if (t.Id != 0)
                throw new NotImplementedException();

            // Временный контекст бд, на одну транзакцию,
            // чтобы можно было откатить изменения.
            int changes; // Количество изменений
            using (UsersPeopleDb tempDb = new UsersPeopleDb(dataBaseNamePath))
            {
                tempDb.Users.Add(t);
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
                db.Users.Add(t).State = EntityState.Unchanged;
            }
            return t;
        }

        public User? Update(User t)
        {
            if (t.Id == 0)
                throw new NotImplementedException();

            // Временный контекст бд, на одну транзакцию,
            // чтобы можно было откатить изменения.
            int changes; // Количество изменений
            using (UsersPeopleDb tempDb = new UsersPeopleDb(dataBaseNamePath))
            {
                tempDb.Users.Update(t);
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
                db.Users.Local.ToObservableCollection().ReplaceOrAdd(users => users.Id == t.Id, t);
                db.Entry(t).State = EntityState.Unchanged;
            }
            return t;
        }

        public bool Delete(long id)
        {
            if (id == 0)
                throw new NotImplementedException();

            // Временный контекст бд, на одну транзакцию,
            // чтобы можно было откатить изменения.
            int changes = 0; // Количество изменений
            using (UsersPeopleDb tempDb = new UsersPeopleDb(dataBaseNamePath))
            {
                var old = tempDb.Users.Find(id);
                if (old is not null)
                {
                    tempDb.Users.Remove(old);
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
                db.Users.Local.ToObservableCollection().RemoveFirst(users => users.Id == id);
            }
            return true;
        }

        public bool IsExistName(string name)
        {
            return db.Users.Any(u => u.Name == name);
        }
    }
}
