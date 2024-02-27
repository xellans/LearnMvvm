using System.ComponentModel;

namespace Standard
{
    public static partial class PropertyDescriptorHelper
    {
        private static readonly Dictionary<Type, PropertyDescriptorCollection> types = new();

        private static PropertyDescriptorCollection GetProperties(Type targetType)
        {
            if (!types.TryGetValue(targetType, out var properties))
            {
                properties = TypeDescriptor.GetProperties(targetType);
                types.Add(targetType, properties);
            }
            return properties;
        }

        public static void SetValue<T>(this T target, string propertyName, object value)
            where T : class
        {
            GetProperties(target.GetType())[propertyName]?.SetValue(target, value);
        }
        public static void SetValue<T>(this T target, PropertyDescriptor propertyDescriptor, object value)
            where T : class
        {
            propertyDescriptor.SetValue(target, value);
        }

        public static object? GetValue<T>(this T target, string propertyName)
            where T : class
        {
            return GetProperties(target.GetType())[propertyName]?.GetValue(target);
        }
        public static object? GetValue<T>(this T target, PropertyDescriptor propertyDescriptor)
            where T : class
        {
            return propertyDescriptor.GetValue(target);
        }
    }

}
