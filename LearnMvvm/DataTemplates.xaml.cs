using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace LearnMvvm
{
    [ContentProperty(nameof(Templates))]
    public class VmTemplateSelector : DataTemplateSelector
    {
        public Dictionary<Type, DataTemplate> Templates { get; set; } = new();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is not null)
            {
                Type type = item.GetType();
                foreach (var template in Templates)
                {
                    if (type == template.Key || type.IsAssignableTo(template.Key))
                    {
                        return template.Value;
                    }
                }
            }
            return base.SelectTemplate(item, container);
        }
    }
}
