using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCore;

namespace LearnMvvm.Model.ViewModel
{
    public class SettingVM :ViewModelBase
    {
        #region Выбор темы
        private ICommand _DarkTheme;
        public ICommand DarkTheme => _DarkTheme ?? new RelayCommand(Set);

        private ICommand _LightTheme;
        public ICommand LightTheme => _LightTheme ?? new RelayCommand(Set);
        #endregion

        #region Выбор языка
        private ICommand _RussianLangue;
        public ICommand RussianLangue => _RussianLangue ?? new RelayCommand(Set);

        private ICommand _EnglishLangue;
        public ICommand EnglishLangue => _EnglishLangue ?? new RelayCommand(Set);
        #endregion
        private void Set(object path)
        {
            string directory = Path.GetDirectoryName(path.ToString()); //Так как по иерахии ресурсы лежат в своих папках, то для обновления ресурсов находим текущую папку
            // Удаляем ресурсы, содержащие названии папки в названии словаря
            var dictionariesToRemove = Application.Current.Resources.MergedDictionaries
                .Where(d => d.Source != null && d.Source.OriginalString.Contains(directory)).ToList();
            foreach (var dictionary in dictionariesToRemove)
                Application.Current.Resources.MergedDictionaries.Remove(dictionary);
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(path.ToString(), UriKind.Relative) });
        }
    }
}
