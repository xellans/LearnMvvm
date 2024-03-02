using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Common.Standard.Interfaces.ViewModel
{
    public interface ISettingVM
    {
        public ICommand LightTheme { get; }
        public ICommand RussianLangue { get; }

        public ICommand EnglishLangue { get; }
        void Set(object path);
    }
}
