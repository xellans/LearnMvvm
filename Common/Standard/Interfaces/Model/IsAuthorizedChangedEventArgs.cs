namespace Common.Standard.Interfaces.Model
{
    public class IsAuthorizedChangedEventArgs : EventArgs
    {
        public bool IsAuthorized { get; }

        public IsAuthorizedChangedEventArgs(bool isAuthorized)
        {
            IsAuthorized = isAuthorized;
        }
    }

}
