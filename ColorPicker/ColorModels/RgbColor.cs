using ColorPicker.ColorModels.ColorComponents;
using System;
using System.Diagnostics;

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
                hueVal = 60 * (((greenFraction - blueFraction) / delta) % 6);
            }
            else if (_maxComponent == greenFraction)
            {
                hueVal = 60 * (((blueFraction - redFraction) / delta) + 2);
            }
            else if (_maxComponent == blueFraction)
            {
                hueVal = 60 * (((redFraction - greenFraction) / delta) + 4);
            }

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

            return new HsvColor((int)(Math.Ceiling(hueVal)), (int)(Math.Ceiling(satVal * 100)), (int)(Math.Ceiling(valVal * 100)));
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

            return new HslColor((int)(Math.Ceiling(hueVal)), (int)(Math.Ceiling(satVal * 100)), (int)(Math.Ceiling(lightnessVal * 100)));
        }

        public CmykColor ToCmykColor()
        {
            double[] fractions = ColorProcessor.CalculateRgbFractions(Red.Value, Green.Value, Blue.Value);

            double keyVal = 1 - Math.Max(fractions[0], Math.Max(fractions[1], fractions[2]));
            double cyanVal = (1 - fractions[0] - keyVal) / (1 - keyVal);
            double magentaVal = (1 - fractions[1] - keyVal) / (1 - keyVal);
            double yellowVal = (1 - fractions[2] - keyVal) / (1 - keyVal);

            return new CmykColor((int)Math.Ceiling(100 * cyanVal), (int)Math.Ceiling(100 * magentaVal),
                (int)Math.Ceiling(100 * yellowVal), (int)Math.Ceiling(100 * keyVal));
        }
    }
}
