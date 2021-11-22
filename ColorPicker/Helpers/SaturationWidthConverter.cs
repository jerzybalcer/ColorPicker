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
    public class SaturationWidthConverter : IMultiValueConverter
    {
        private double pickerWidth = 0;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            pickerWidth = (double)values[1];
            int saturation = (int)values[0];

            double percentX = (double)saturation / 100.0d;

            double pointX = pickerWidth * percentX;

            return pointX;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {

            double canvasLeft = (double)value;
            object[] ret = new object[1];

            double percentX = canvasLeft / pickerWidth * 100.0d;

            ret[0] = (int)percentX;

            return ret;
        }

    }
}
