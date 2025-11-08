using Music.MD.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;

namespace Music.MD.Markup
{
    [MarkupExtensionReturnType(typeof(string))]
    public class LocExtension : MarkupExtension
    {
        public string ResxKey { get; set; }

        static LocExtension()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Binding binding = new Binding($"[{ResxKey}]")  // 绑定到索引器
            {
                Source = I18nBindingSource.Instance,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            return binding.ProvideValue(serviceProvider);
        }
    }
}