using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Entity;

namespace DataBase.Command
{
    public class CommandPeople
    {
        public CommandPeople()
        {
            using UsersPeopleDb db = new ApplicationContext();
            db.Database.EnsureCreated();
        }
        public void CreatePeople()
        {
            string[] peopleArray = { "Алиса", "Екатерина", "Василий", "Андрей", "Пётр", "Инна", "Вика", "Жанна", "Ксюша", "Анатолий" };
            using UsersPeopleDb db = new ApplicationContext();
            for (int i = 0; i < 10; i++)
            {
                People people = new People()
                {
                    Name = peopleArray[i],
                    Age = Random.Shared.Next(18, 26),
                };
                db.People.Add(people);
            }
            db.SaveChanges();
        }

        public List<People> OutputPeople()
        {
            using UsersPeopleDb db = new ApplicationContext();
            return db.People.ToList();
        }
    }
}
