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
            using ApplicationContext db = new ApplicationContext();
            db.Database.EnsureCreated();
        }
        public void CreatePeople()
        {
            string[] peopleArray = { "Алиса", "Екатерина", "Василий", "Андрей", "Пётр", "Инна", "Вика", "Жанна", "Ксюша", "Анатолий" };
            using ApplicationContext db = new ApplicationContext();
            for (int i = 0; i < 200; i++)
            {
                People people = new People()
                {
                    Name = peopleArray[Random.Shared.Next(0, 9)],
                    CompletedTasks = Random.Shared.Next(10, 1000),
                    RemainsExecute = Random.Shared.Next(10, 1000)
                };
                db.People.Add(people);
            }
            db.SaveChanges();
        }

        public List<People> OutputPeople()
        {
            using ApplicationContext db = new ApplicationContext();
            return db.People.ToList();
        }
    }
}
