using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop_cha_qaqc_phase2.ValueConverter
{
    public class IntToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string temp = @"\Resources\Images\0_Wait"; 
            switch (value)
            {
                case 0: 
                    temp = @"\Resources\Images\0_Wait.png";
                    break;
                case 1:
                    temp = @"\Resources\Images\1_Man.png";
                    break;
                case 2:
                    temp = @"\Resources\Images\2_Auto.png";
                    break;
                case 3:
                    temp = @"\Resources\Images\3_Warning.png";
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
