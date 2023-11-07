using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MvvmClientExample.Converters
{
    public class ButtonStatusConverter : IValueConverter
    {
        public Style SuccessStyle { get; set; }
        public Style ErrorStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var goodStyle = new Style(typeof(Button), SuccessStyle);

            goodStyle.Setters.Add(new Setter()
            {
                Property = ContentControl.ContentProperty,
                Value = "Включить двухфакторную аутентификацию"
            });

            var badStyle = new Style(typeof(Button), ErrorStyle);

            badStyle.Setters.Add(new Setter()
            {
                Property = ContentControl.ContentProperty,
                Value = "Выключить двухфакторную аутентификацию"
            });

            var btn = value as string;

            if (btn == "Выключена")
                return goodStyle;
            else
                return badStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
