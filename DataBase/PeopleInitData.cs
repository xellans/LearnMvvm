namespace DataBase
{
    public class PeopleInitData
    {
        #region Данные для заполнения новой БД.
        private static readonly string[] peopleArray = "Алиса Екатерина Василий Андрей Пётр Инна Вика Жанна Ксюша Анатолий".Split(' ', StringSplitOptions.RemoveEmptyEntries);
        public static IEnumerable<Person> CreatePeople()
        {
            for (int i = 1; i <= 200; i++)
            {
                Person people = new Person()
                {
                    Id = i,
                    Name = peopleArray[Random.Shared.Next(0, 9)],
                    CompletedTasks = Random.Shared.Next(10, 1000),
                    RemainsExecute = Random.Shared.Next(10, 1000)
                };
                yield return people;
            }
        }
        #endregion
    }

}
