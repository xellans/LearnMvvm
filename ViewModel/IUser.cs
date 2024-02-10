using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel.VmHellper;

namespace ViewModel
{
    public interface IUser: INotifyPropertyChanged
    {
        public User? User { get; set; }
      //  public ICommand AddUserCommand { get; set; }
    }
}
