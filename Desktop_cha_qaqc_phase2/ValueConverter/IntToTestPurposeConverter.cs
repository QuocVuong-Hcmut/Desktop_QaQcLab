using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop_cha_qaqc_phase2.ValueConverter
{
    public class OtherTargetPurposeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           switch ((int)value)
            {
                case 0:return "Định kỳ"; break;
                case 1:return "Bất thường"; break;
                case 2:return "SP mới"; break;
                case 3:return "Khác"; break;
                default: return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           switch ((string)value)
            {
                case "Định kỳ": return 0; break;
                case "Bất thường": return 1; break;
                case "SP mới": return 2; break;
                case "Khác": return 3; break;
                default: return null;
            }
        }
    }
}
