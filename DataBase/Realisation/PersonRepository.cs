using Common.Standard.Interfaces.Model;
using DataBase;
using DataBase.Interfaces;
using DataBase.Realisation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Realisation
{
    public class PersonRepository : IPersonRepository
    {
        public Command<Person> Command { get; set; }

        /// <summary>
        /// Список людей
        /// </summary>
        /// <returns></returns>
        private IReadOnlyObservableCollection<IPerson>? products;
        public IReadOnlyObservableCollection<IPerson> PersonCollections()
        {
            if (products is null)
            {
                var list = Command.ToObservableCollections();
                products = new ReadOnlyObservableList<IPerson, Person>(list);
            }
            return products;
        }
        public PersonRepository()
        {
            Command = new Command<Person>();
            Command.Load();
            CreatePerson();
        }

        #region Заполнение бд, нужно только для примера.
        public void CreatePerson()
        {
            if (PersonCollections().Count() > 0)
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
