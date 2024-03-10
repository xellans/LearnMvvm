namespace LearnMvvm
{
    public static class InstancesProvider
    {
        private static readonly Dictionary<Type, Delegate> creators = new();
        private static readonly Dictionary<Type, object?> instances = new();

        public static void Register<T>(Func<T?> creator)
           where T : class
        {
            ArgumentNullException.ThrowIfNull(creator, nameof(creator));
            creators[typeof(T)] = creator;
            instances.Remove(typeof(T));
        }

        public static T? GetInstance<T>()
           where T : class
        {
            if (instances.TryGetValue(typeof(T), out object? obj))
            {
                return (T?)obj;
            }
            if (!creators.TryGetValue(typeof(T), out Delegate? @delegate))
            {
                return null;
            }

            Func<T?> creator = (Func<T?>)@delegate ?? throw new NullReferenceException();
            T? inst = creator();
            instances[typeof(T)] = inst;
            return inst;
        }
    }
}
