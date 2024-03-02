using System.Diagnostics.CodeAnalysis;

namespace Standard
{
    public class EqualityComparer<T> : System.Collections.Generic.EqualityComparer<T>
    {
        protected readonly Func<T, T, bool> equals;
        protected readonly Func<T, int> getHashCode;
        protected EqualityComparer(Func<T, T, bool> equals, Func<T, int>? getHashCode = default)
        {
            this.equals = equals;
            this.getHashCode = getHashCode ?? new Func<T, int>(t => t?.GetHashCode() ?? 0);
        }
        public override bool Equals(T? x, T? y)
        {
            if (x is null || y is null)
            {
                return x is null && y is null;
            }
            return equals(x, y);
        }

        public override int GetHashCode([DisallowNull] T obj) => getHashCode(obj);

        public static EqualityComparer<T> Create(Func<T, T, bool> equals, Func<T, int>? getHashCode = default)
            => new(equals, getHashCode);
    }
}
