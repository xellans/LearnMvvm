using Common.Standard.Interfaces.Model;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Authorized
{
    public class InitPeopleRepository : IReadOnlyRepository<IPerson>
    {
        private readonly ReadOnlyObservableList<IPerson> people;
        private readonly ObservableCollection<IPerson> peoplelist = new();

        public InitPeopleRepository()
        {
            people = new(peoplelist);
        }

        public bool Any(Expression<Func<IPerson, bool>> expression)
        {
            return Queryable.AsQueryable(people.Cast<IPerson>()).Any(expression);
        }

        public IPerson? FirstOrDefault()
        {
            if (people.Count == 0)
                return null;
            return people[0];
        }

        public IPerson? FirstOrDefault(Expression<Func<IPerson, bool>> expression)
        {
            return Queryable.AsQueryable(people.Cast<IPerson>()).FirstOrDefault(expression);
        }

        public void Load()
        {
            peoplelist.Clear();
            var people = Enumerable.Range(1, 200).Select(i => new PersonDto
            {
                Id = i,
                Name = peopleArray[Random.Shared.Next(peopleArray.Length)],
                CompletedTasks = Random.Shared.Next(10, 1000),
                RemainsExecute = Random.Shared.Next(10, 1000)
            }).ToArray();
            foreach (var person in people)
            {
                peoplelist.Add(person);
            }
        }
        private static readonly string[] peopleArray = { "Алиса", "Екатерина", "Василий", "Андрей", "Пётр", "Инна", "Вика", "Жанна", "Ксюша", "Анатолий" };
        public IReadOnlyObservableCollection<IPerson> ToObservableCollections()
        {
            return people;
        }

        public IQueryable<IPerson> Where(Expression<Func<IPerson, bool>> expression)
        {
            return Queryable.AsQueryable(people.Cast<IPerson>()).Where(expression);
        }
    }
}
