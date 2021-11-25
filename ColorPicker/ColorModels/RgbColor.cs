using ColorPicker.ColorModels.ColorComponents;
using System;
using System.Diagnostics;
using System.Windows.Media;

namespace ColorPicker.ColorModels
{
    public class RgbColor
    {
        public EightBitComponent Red { get; set; }
        public EightBitComponent Green { get; set; }
        public EightBitComponent Blue { get; set; }

        private double _maxComponent, _minComponent;

        public RgbColor(int red, int green, int blue)
        {
            Red = new EightBitComponent(red);
            Green = new EightBitComponent(green);
            Blue = new EightBitComponent(blue);
        }

        private double CalculateHue()
        {
            double redFraction = (double)Red.Value / 255.0d;
            double greenFraction = (double)Green.Value / 255.0d;
            double blueFraction = (double)Blue.Value / 255.0d;

            _maxComponent = Math.Max(redFraction, Math.Max(greenFraction, blueFraction));
            _minComponent = Math.Min(redFraction, Math.Min(greenFraction, blueFraction));

            double delta = _maxComponent - _minComponent;

            double hueVal = 0;

            if (delta == 0)
            {
                hueVal = 0;
            }
            else if (_maxComponent == redFraction)
            {
                hueVal = 60 * (((greenFraction - blueFraction) / delta) %6);
            }
            else if (_maxComponent == greenFraction)
            {
                hueVal = 60 * (((blueFraction - redFraction) / delta) + 2);
            }
            else if (_maxComponent == blueFraction)
            {
                hueVal = 60 * (((redFraction - greenFraction) / delta) + 4);
            }

            if (hueVal < 0)
                hueVal = hueVal + 360;

            return hueVal;
        }

        public HsvColor ToHsvColor()
        {
            double hueVal = CalculateHue();
            double delta = _maxComponent - _minComponent;

            double satVal;

            if (_maxComponent == 0)
            {
                satVal = 0;
            }
            else
            {
                satVal = delta / _maxComponent;
            }

            double valVal = _maxComponent;

            return new HsvColor((int)(hueVal), (int)(satVal * 100), (int)(valVal * 100));
        }

        public HslColor ToHslColor()
        {
            double hueVal = CalculateHue();
            double delta = _maxComponent - _minComponent;

            double lightnessVal = (_maxComponent + _minComponent) / 2;

            double satVal;

            if (_maxComponent == 0)
            {
                satVal = 0;
            }
            else
            {
                satVal = delta / (1 - Math.Abs(2 * lightnessVal - 1));
            }

            return new HslColor((int)(hueVal), (int)(satVal * 100), (int)(lightnessVal * 100));
        }

        public CmykColor ToCmykColor()
        {
            double redFraction = (double)Red.Value / 255.0d;
            double greenFraction = (double)Green.Value / 255.0d;
            double blueFraction = (double)Blue.Value / 255.0d;

            double keyVal = 1 - Math.Max(redFraction, Math.Max(greenFraction, blueFraction));
            double cyanVal = (1 - redFraction - keyVal) / (1 - keyVal);
            double magentaVal = (1 - greenFraction - keyVal) / (1 - keyVal);
            double yellowVal = (1 - blueFraction - keyVal) / (1 - keyVal);

            return new CmykColor((int)(100 * cyanVal), (int)(100 * magentaVal),
                (int)(100 * yellowVal), (int)(100 * keyVal));
        }

        public SolidColorBrush ToSolidColorBrush(int alphaVal = 255)
        {
            return new SolidColorBrush(
                Color.FromArgb((byte)alphaVal, (byte)Red.Value, (byte)Green.Value, (byte)Blue.Value)
                );
        }

        public string ToHexString(bool isAlphaIncluded, int alphaVal = 255)
        {
            string alphaHex = "";

            if (isAlphaIncluded)
            {
                alphaHex = alphaVal.ToString("X2");
            }

            return "#" + alphaHex + Red.Value.ToString("X2") + Green.Value.ToString("X2") + Blue.Value.ToString("X2");
        }
    }
}
