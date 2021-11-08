using System;

namespace ColorPicker
{
    public static class ColorProcessor
    {
        private const double RedBrightnessConst = 0.241;
        private const double GreenBrightnessConst = 0.691;
        private const double BlueBrightnessConst = 0.068;

        public static double CalculateBrightness(int red, int green, int blue)
        {
            return Math.Sqrt(red * red * RedBrightnessConst + green * green * GreenBrightnessConst + blue * blue * BlueBrightnessConst);
        }
    }
}
