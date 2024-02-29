using DataBase;
using DataBase.Realisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Realisation
{
    public class UserRepository
    {
        public Command<User> Command { get; set; }
        public UserRepository() 
        {
            Command = new Command<User>(); 
        }
    }
}
