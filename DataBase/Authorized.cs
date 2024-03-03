using Common.Standard.Interfaces.Model;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repositories
{
    public class Authorized : IAuthorized
    {
        public Authorized(Context context)
        {
            this.context = context;
            //context.Database.EnsureCreated();
            User? old = context.User.FirstOrDefault();
            if (old != null)
                IsAuthorized = old.IsAuthorized;
        }
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
