using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Standard.Interfaces.Model
{
    public interface IAuthorized
    {
        bool IsAuthorized { get; }

        event EventHandler<IsAuthorizedChangedEventArgs>? AuthorizedChanged;

        void Authorize(string? name);
    }
}
