using Common.Standard.Interfaces.ViewModel;
using System.ComponentModel;
using WpfCore;

namespace LearnMvvm
{
    // Это классы уровня View
    public class NavigatorLocator : ViewModelBase
    {
        public static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new());
        public IAuthVM? AuthVM { get => Get<IAuthVM>(); set => Set(value); }

        public NavigatorLocator()
        {
            if (IsInDesignMode)
            {
                AuthVM = new AuthVMDesignMode();
            }
        }

        public object? CurrentContext { get => Get<object>(); set => Set(value); }

        public RelayCommand SetCurrentContext => GetCommand<object?>(obj =>
        {
            if (nameof(AuthVM).Equals(obj))
                obj = AuthVM;
            CurrentContext = obj;
        });
    }

    // Тестовый класс для режима разработки.
    internal class AuthVMDesignMode : IAuthVM
    {
        public RelayCommand AuthorizeCommand { get; } = new RelayCommand(() => { });
        public long Id { get; set; } = 12344;
        public bool IsAuthorized { get; } = false;
        public string? Name { get; set; } = "Абра Катабра";
    }
}
