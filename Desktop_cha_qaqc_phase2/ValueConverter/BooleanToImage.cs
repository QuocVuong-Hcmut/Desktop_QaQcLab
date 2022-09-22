using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop_cha_qaqc_phase2.ValueConverter
{
    public class BooleanToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string temp = ""; 
            switch ((bool)value)
            {
               
                case false:
                    temp = @"\Resources\Images\1_Man.png";
                    break;
                case true:
                    temp = @"\Resources\Images\2_Auto.png";
                    break;
            }
            return temp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           throw new NotImplementedException(); 
        }
    }
}
