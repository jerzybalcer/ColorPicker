using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ColorPicker.Helpers
{
    public class ValueHeightConverter : IMultiValueConverter
    {
        private double pickerHeight = 0;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            pickerHeight = (double)values[1];
            int saturation = (int)values[0];

            double percentY = (double)saturation / 100.0d;

            double pointY = pickerHeight * percentY;

            return pickerHeight - pointY;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {

            double canvasTop = (double)value;
            object[] ret = new object[1];

            double percentY = canvasTop / pickerHeight * 100.0d;

            ret[0] = (int)(pickerHeight - (pickerHeight - percentY));

            return ret;
        }

    }
}
