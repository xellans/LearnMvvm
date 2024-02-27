using System.Windows;
using static System.Windows.Media.VisualTreeHelper;

namespace Common.WpfCore
{
    public static partial class VisualTreeHelper
    {
        public static T? FindAncestor<T>(this DependencyObject dobj)
            where T : DependencyObject
        {
            while (dobj is not null)
            {
                if (dobj is T t)
                    return t;

                dobj = GetParent(dobj);
            }
            return null;
        }

        public static FrameworkElement? FindDataAncestor<TData>(this DependencyObject dobj)
        {
            while (dobj is not null)
            {
                if (dobj is FrameworkElement element and { DataContext: TData })
                    return element;

                dobj = GetParent(dobj);
            }
            return null;
        }

        public static TData? FindData<TData>(this DependencyObject dobj)
        {
            FrameworkElement? element = dobj.FindDataAncestor<TData>();
            if (element == null)
                return default;
            return (TData?)element.DataContext;
        }

    }

}
