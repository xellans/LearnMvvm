using WpfCore;
using Common.Standard.Interfaces.ViewModel;
using Common.Standard.Interfaces.Model;

namespace ViewModel
{
    public class AuthVM : ViewModelBase, IAuthVM
    {
        private readonly IAuthorized authorized;

        public AuthVM(IAuthorized authorized)
        {
            this.authorized = authorized;

            authorized.AuthorizedChanged += OnAuthorizedChanged;

            AuthorizeCommand = new RelayCommand(() =>
            {
                authorized.Authorize(Name);
            });
        }

        private void OnAuthorizedChanged(object? sender, IsAuthorizedChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsAuthorized));
        }

        public RelayCommand AuthorizeCommand { get; }

        public long Id { get => Get<long>(); set => Set(value); }

        public string? Name { get => Get<string>(); set => Set(value); }

        public bool IsAuthorized => authorized.IsAuthorized;

    }
}
