using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Authorized
    {
        public Authorized() =>  Context = new();
        private Context Context;

        public bool IsAuthorized { get; private set; }

        public event EventHandler<IsAuthorizedChangedEventArgs>? AuthorizedChanged;

        public void Authorize(string? name)
        {
            IsAuthorizedChangedEventArgs args = new IsAuthorizedChangedEventArgs(!string.IsNullOrEmpty(name));
            if (args.IsAuthorized)
            {
                User? old = Context.User.FirstOrDefault(x => x.Name == name);
                if (old is null)
                {
                    Context.User.Add(new User() { Name = name, IsAuthorized = true });
                    Context.SaveChanges();
                }
            }
            IsAuthorized = args.IsAuthorized;
            AuthorizedChanged?.Invoke(this, args);
        }
    }
    public class IsAuthorizedChangedEventArgs : EventArgs
    {
        public bool IsAuthorized { get; }

        public IsAuthorizedChangedEventArgs(bool isAuthorized)
        {
            IsAuthorized = isAuthorized;
        }
    }
}
