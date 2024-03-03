using Common.Standard.Interfaces.Model;
using DataBase;

namespace Repositories
{
    public class PersonModel: IPersonModel
    {
        public IRepository<IPerson> Repository { get; set; }
        public IReadOnlyObservableCollection<IPerson> PersonCollections { get; set; }

        public PersonModel(IRepository<IPerson> Repository) 
        {
            this.Repository = Repository;
            PersonCollections = Repository.ToObservableCollections();
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
                Repository.Add(people);
            }
        }
        #endregion
    }
}
