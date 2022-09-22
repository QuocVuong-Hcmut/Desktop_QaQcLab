using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop_cha_qaqc_phase2.ValueConverter
{
    public class IntToTargetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           switch ((int)value)
            {
                case 0:return "Báo cáo"; break;
                case 1:return "Cảnh báo"; break;
                default: return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           switch ((string)value)
            {
                case "Báo cáo": return 0; break;
                case "Cảnh báo": return 1; break;
               
                default: return 0;
            }
        }
    }
}
