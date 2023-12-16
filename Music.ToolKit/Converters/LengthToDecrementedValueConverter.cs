using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Music.ToolKit.Converters
{
    public class LengthToDecrementedValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int length = 16;
            if (value is int textLength)
            {
                if (textLength < length)
                {
                    return length - textLength;
                }
                else
                {
                    return 0;
                }
            }
           
            return length; // 默认值为 16
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
