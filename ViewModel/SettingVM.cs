﻿using Common.Standard.Interfaces.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCore;

namespace ViewModel
{
    public class SettingVM : ViewModelBase, ISettingVM
    {
        #region Выбор темы
        public ICommand DarkTheme => GetCommand<object>(Set);

        public ICommand LightTheme => GetCommand<object>(Set);
        #endregion

        #region Выбор языка
        private ICommand _RussianLangue;
        public ICommand RussianLangue => _RussianLangue ?? new RelayCommand(Set);

        private ICommand _EnglishLangue;
        public ICommand EnglishLangue => _EnglishLangue ?? new RelayCommand(Set);
        #endregion
        public void Set(object path)
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
