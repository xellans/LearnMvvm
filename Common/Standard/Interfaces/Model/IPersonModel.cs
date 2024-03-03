using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Standard.Interfaces.Model
{
    public interface IPersonModel
    {
        public IRepository<IPerson> Repository { get; }
        public IReadOnlyObservableCollection<IPerson> PersonCollections { get; }
        void CreatePerson();
    }
}
