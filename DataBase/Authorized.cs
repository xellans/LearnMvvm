using Common.Standard.Interfaces.Model;

namespace DataBase
{
    public class Authorized : IAuthorized
    {
        public Authorized(Context context) => this.context = context;
        private readonly Context context;

        public bool IsAuthorized { get; private set; }

        public event EventHandler<IsAuthorizedChangedEventArgs>? AuthorizedChanged;

        public void Authorize(string? name)
        {
            IsAuthorizedChangedEventArgs args = new IsAuthorizedChangedEventArgs(!string.IsNullOrEmpty(name));
            if (args.IsAuthorized)
            {
                User? old = context.User.FirstOrDefault(x => x.Name == name);
                if (old is null)
                {
                    context.User.Add(new User() { Name = name, IsAuthorized = true });
                    context.SaveChanges();
                }
            }
            IsAuthorized = args.IsAuthorized;
            AuthorizedChanged?.Invoke(this, args);
        }
    }

}