using Entity;
using Interfaces;

namespace DataBase.Repositories
{
    public partial class PeopleModel : IPeopleModel
    {
        public bool IsAuthorized { get; private set; }

        public event EventHandler<IsAuthorizedChangedEventArgs>? AuthorizedChanged;

        public void Authorize(string? name)
        {
            IsAuthorizedChangedEventArgs args = new IsAuthorizedChangedEventArgs(!string.IsNullOrEmpty(name));
            if (args.IsAuthorized)
            {
                User? old = UsersRepository.GetCollection().FirstOrDefault();
                if (old is null)
                {
                    UsersRepository.Add(new User() { Name = name });
                }
                else
                {
                    old.Name = name!;
                    UsersRepository.Update(old);
                }
            }
            else
            {
                db.Users.Local.ToObservableCollection().Clear();
                db.SaveChanges();
            }

            IsAuthorized = args.IsAuthorized;
            AuthorizedChanged?.Invoke(this, args);
        }
    }
}
