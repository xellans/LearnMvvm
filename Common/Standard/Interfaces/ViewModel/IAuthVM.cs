using WpfCore;

namespace Common.Standard.Interfaces.ViewModel
{
    public interface IAuthVM
    {
        RelayCommand AuthorizeCommand { get; }
        long Id { get; set; }
        bool IsAuthorized { get; }
        string? Name { get; set; }
    }
}
