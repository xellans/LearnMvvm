using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Command
{
    public class CommandUser
    {
        public CommandUser()
        {
            using ApplicationContext db = new ApplicationContext();
            db.Database.EnsureCreated();
        }

        /// <summary>
        /// Добавить пользователя в бд
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            if (IsExistUser(user.Name))
                return;
            using (var db = new ApplicationContext())
            {
                db.User.Add(user);
                    db.SaveChanges();
            }
        }

        public User? GetUser()
        {
            User? user = null;
            using (var db = new ApplicationContext())
            {
                user = db.User.FirstOrDefault();
            }
            return user;
        }

        /// <summary>
        /// Проверка на существование пользователя в бд
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool IsExistUser(string Name)
        {
            bool _IsExistUser = false;
            using (var db = new ApplicationContext())
            {
                _IsExistUser = db.User.Any(x => x.Name == Name);
            }
            return _IsExistUser;
        }

        /// <summary>
        /// Проверка авторизован пользователь или нет
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsAuthorized(User user)
        {
            bool IsAuth = false;
            using (var db = new ApplicationContext())
            {
                var _user = db.User.FirstOrDefault(x => x.Name == user.Name);
                if (_user != null)
                    IsAuth = _user.IsAuthorized;
            }
            return IsAuth;
        }
    }
}
