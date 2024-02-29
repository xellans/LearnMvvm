using Common.Standard.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Standard.Interfaces.ViewModel
{
    public interface IPersonVM
    {
        public ObservableCollection<IPerson> PersonList { get; set; }
        public IRepository<IPerson> people { get; set; }
    }
}
