using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories
{
    public class PeopleModel : IPeopleModel
    {
        internal readonly UsersPeopleDb db;
        internal readonly string dataBaseNamePath;

        public PeopleModel(string dataBaseNamePath)
        {
            this.dataBaseNamePath = dataBaseNamePath;
            string path = Path.GetDirectoryName(dataBaseNamePath) ?? string.Empty;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            db = new UsersPeopleDb(dataBaseNamePath);
            db.Database.EnsureCreated();

            UsersRepository = new UsersRepository(db, dataBaseNamePath);
            PeopleRepository = new PeopleRepository(db, dataBaseNamePath);
            db.Users.Load();
            db.People.Load();
        }

        public IUsersRepository UsersRepository { get; }
        public IPeopleRepository PeopleRepository { get; }
    }
}
