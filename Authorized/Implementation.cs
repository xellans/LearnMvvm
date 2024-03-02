using Common.Standard.Interfaces.Model;
using System.Diagnostics;
using System.Text.Json;

namespace Authorized
{
    public class Implementation : IAuthorized, IDisposable
    {
        private readonly FileStream file;
        private readonly Dictionary<string, User> users;

        public Implementation(string fileName)
        {
            file = File.Open(fileName, FileMode.OpenOrCreate);
            List<User>? users;
            try
            {
                users = JsonSerializer.Deserialize<List<User>>(file);
            }
            catch (Exception)
            {
                users = null;
            }

            if (users is null)
            {
                users = new List<User>();
                file.SetLength(0);
                JsonSerializer.Serialize(file, users);
                file.Flush();
            }

            this.users = users.ToDictionary(usr => usr.Name,
                Standard.EqualityComparer<string>.Create(
                    (x, y) => x.Equals(y, StringComparison.CurrentCultureIgnoreCase),
                    x => string.GetHashCode(x, StringComparison.CurrentCultureIgnoreCase)));
            IsAuthorized = users.Any(usr => usr.IsAuthorized);
        }

        public bool IsAuthorized { get; private set; }

        public event EventHandler<IsAuthorizedChangedEventArgs>? AuthorizedChanged;

        public void Authorize(string? name)
        {
            if (IsDisposed)
            {
                throw new Exception("Объект уничтожен");
            }
            IsAuthorizedChangedEventArgs args = new IsAuthorizedChangedEventArgs(!string.IsNullOrEmpty(name));
            if (args.IsAuthorized)
            {

                if (!users.TryGetValue(name!, out User? user))
                {
                    users[name!] = user = new User() { Name = name!, IsAuthorized = true };
                }

                user.IsAuthorized = true;

                file.SetLength(0);
                JsonSerializer.Serialize(file, users.Values);
                file.Flush();
            }
            IsAuthorized = args.IsAuthorized;
            AuthorizedChanged?.Invoke(this, args);
        }

        public bool IsDisposed { get; private set; }
        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;
            file.Dispose();
            GC.SuppressFinalize(this);
        }
        ~Implementation() => Dispose();
    }

    internal class User
    {
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Возвращает true если пользователь авторизирован
        /// </summary>
        public bool IsAuthorized { get; set; }
    }

}
