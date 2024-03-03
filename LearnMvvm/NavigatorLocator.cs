using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfCore;

namespace LearnMvvm.Model.ViewModel
{
    // Это классы уровня View
    public class NavigatorLocator : ViewModelBase
    {
        #region Эта часть функционала локатора
        public IAuthVM? AuthVM { get => Get<IAuthVM>(); set => Set(value); }
        
        public IPersonVM? PersonVM { get => Get<IPersonVM>(); set => Set(value); }

        public IProductVM? ProductVM { get => Get<IProductVM>(); set => Set(value); }
        public ISettingVM? SettingVM { get => Get<ISettingVM>(); set => Set(value); }
        #endregion

        #region Это часть функционала навигатора
        public object? CurrentContext { get => Get<object>(); set => Set(value); }

        public RelayCommand SetCurrentContext => GetCommand<object?>(obj =>
        {
            if (nameof(PersonVM).Equals(obj))
            {
                //PersonVM = new PersonVM();
                obj = PersonVM;
            }

            if (nameof(ProductVM).Equals(obj))
            {
                //ProductVM = new ProductVM();
                obj = ProductVM;
            }

            if (nameof(SettingVM).Equals(obj))
            {
                //SettingVM = new SettingVM();
                obj = SettingVM;
            }

            CurrentContext = obj;
        });
        #endregion

        public NavigatorLocator()
        {
            //IAuthorized authorized = new Authorized();
            //AuthVM = new AuthVM(authorized);

            // Инициализация для режима разработки
            if(IsInDesignMode)
            {
                AuthVM = new AuthVMDesignMode();
                PersonVM = new PersonVMDesignMode();
                ProductVM = new ProductVMDesignMode();
            }
        }

        /// <summary><see langword="true"/> - если находится в режиме разработки.
        /// Необходимо для создания данных Времени Разработки,
        /// так как в это время могут быть дуступны не все типы и ресурсы.</summary>
        public static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new());


        #region AP-свойство для упрощения привязки к навигатору в XAML
        public static NavigatorLocator GetNavigator(DependencyObject obj)
        {
            return (NavigatorLocator)obj.GetValue(NavigatorProperty);
        }

        public static void SetNavigator(DependencyObject obj, NavigatorLocator value)
        {
            obj.SetValue(NavigatorProperty, value);
        }

        // Using a DependencyProperty as the backing store for Navigator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigatorProperty =
            DependencyProperty.RegisterAttached("Navigator", typeof(NavigatorLocator), typeof(NavigatorLocator), new PropertyMetadata(null));

        #endregion
    }

    // Тестовый класс для режима разработки.
    internal class AuthVMDesignMode : IAuthVM
    {
        public RelayCommand AuthorizeCommand { get; } = new RelayCommand(() => { });
        public long Id { get; set; } = 12344;
        public bool IsAuthorized { get; } = false;
        public string? Name { get; set; } = "Абра Катабра";
    }
    // Тестовый класс для режима разработки.
    internal class PersonVMDesignMode : IPersonVM
    {
        public IReadOnlyObservableCollection<IPerson> PersonList { get; } = new ReadOnlyObservableList<IPerson, Person>(new ObservableCollection<Person>()
        {
            new() {Name = "First"},
            new() {Name = "Second"}
        });
        public RelayCommand Refresh { get; } = new RelayCommand(() => { });
    }
    internal class Person : IPerson
    {
        public string Name { get; set; } = string.Empty;
        public int CompletedTasks { get; set; }
        public int RemainsExecute { get; set; }
        public long Id { get; }
    }
    // Тестовый класс для режима разработки.
    internal class ProductVMDesignMode : IProductVM
    {
        public ICommand Add { get; } = new RelayCommand(() => { });
        public ICommand Delete { get; } = new RelayCommand(() => { });
        public IReadOnlyObservableCollection<IProduct> ProductDataList { get; } = new ReadOnlyObservableList<IProduct, Product>(new ObservableCollection<Product>()
        {
            new() {Name = "First"},
            new() {Name = "Second"}
        });
        public ICommand SaveEdit { get; } = new RelayCommand(() => { });
        public IProduct? Selected { get; set; }
    }

    internal class Product : IProduct
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long Id { get; set; }
    }
}
