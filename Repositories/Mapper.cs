using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public static class Mapper
    {
        /// <summary>
        /// Копирование свойств из одного класса в другой
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void CopyProperties(object source, object destination)
        {
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationType.GetProperty(sourceProperty.Name);

                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    object value = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);
                }
            }
        }
    }
}
