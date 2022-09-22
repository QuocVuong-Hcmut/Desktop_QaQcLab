using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop_cha_qaqc_phase2.ValueConverter
{
    public class BooleanToButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return (bool)value? "Đang sửa" : "Chỉnh sửa";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           throw new NotImplementedException(); 
        }
    }
}
