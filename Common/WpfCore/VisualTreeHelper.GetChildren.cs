using System.Windows;
using static System.Windows.Media.VisualTreeHelper;

namespace Common.WpfCore
{
    public static partial class VisualTreeHelper
    {
        public static IEnumerable<DependencyObject> GetChildren(this DependencyObject parent)
        {
            ArgumentNullException.ThrowIfNull(parent);
            Queue<DependencyObject> queue = new Queue<DependencyObject>(16);
            queue.Enqueue(parent);
            while (queue.Count != 0)
            {
                DependencyObject current = queue.Dequeue();
                yield return current;

                int count = GetChildrenCount(current);
                for (int i = 0; i < count; i++)
                {
                    queue.Enqueue(GetChild(current, i));
                }
            }
        }
        public static IEnumerable<T> GetChildren<T>(this DependencyObject parent)
           where T : DependencyObject
            => parent.GetChildren().OfType<T>();

        public static T? GetFirstChild<T>(this DependencyObject parent)
           where T : DependencyObject
            => (T?)parent.GetChildren().FirstOrDefault(child => child is T);

    }

}
