using DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Realisation
{
    public class PersonRepository
    {
        public Command<Person> Command { get; set; }

        /// <summary>
        /// Список людей
        /// </summary>
        /// <returns></returns>
        public ReadOnlyObservableCollection<Person> PersonCollections;
        public PersonRepository()
        {
            Command = new Command<Person>();
            Command.Load();
            PersonCollections = Command.ToObservableCollections();
            CreatePerson();
        }

        #region Заполнение бд, нужно только для примера.
        public void CreatePerson()
        {
            if (PersonCollections.Count() > 0)
                return;
            string[] peopleArray = { "Алиса", "Екатерина", "Василий", "Андрей", "Пётр", "Инна", "Вика", "Жанна", "Ксюша", "Анатолий" };
            for (int i = 0; i < 200; i++)
            {
                Person people = new Person()
                {
                    Name = peopleArray[Random.Shared.Next(0, 9)],
                    CompletedTasks = Random.Shared.Next(10, 1000),
                    RemainsExecute = Random.Shared.Next(10, 1000)
                };
                Command.Add(people);
            }
        }
        #endregion
    }
}
