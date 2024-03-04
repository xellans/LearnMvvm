using Common.Standard.Interfaces.Model;
using Common.WpfCore;
using DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repositories
{
    public class Authorized: IAuthorized
    {
        public Authorized(Context context) 
        {
            //Context = new();
            this.context = context;
            //this.context.Database.EnsureCreated();
            User? old = this.context.User.FirstOrDefault();
            if (old != null)
                IsAuthorized = old.IsAuthorized;
        }
        private Context context;

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
                    // Context.User.Add(user);
                    //            Context.User.Add(Context.CreateProxy<User>(
                    //p =>
                    //{
                    //    p.Name = name;
                    //    p.IsAuthorized = true;
                    //}));


                    var user = context.CreateProxy<User>();
                    user.IsAuthorized = true;
                    user.Name = name;
                    context.Add(user);
                    context.SaveChanges();
                }
            }
            IsAuthorized = args.IsAuthorized;
            AuthorizedChanged?.Invoke(this, args);
        }

    }
}
