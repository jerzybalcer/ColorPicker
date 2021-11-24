using ColorPicker.ColorModels;
using ColorPicker.ColorModels.ColorComponents;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ColorPicker
{
    public class PickerViewModel : INotifyPropertyChanged
    {
        public SolidColorBrush PickedColor { get; set; }
        public SolidColorBrush ContrastingColor { get; set; }
        public string PickedColorHex { get; set; }
        public RgbColor RgbColor { get; set; }
        public HsvColor HsvColor { get; set; }
        public HslColor HslColor { get; set; }
        public CmykColor CmykColor { get; set; }
        public EightBitComponent Alpha { get; set; }
        public Color PickedHue { get; set; }
        public SolidColorBrush PickingEllipseColor { get; set; }
        public SolidColorBrush ColorCombination1 { get; set; }
        public SolidColorBrush ColorCombination2 { get; set; }
        public SolidColorBrush ColorCombination3 { get; set; }
        public string Combination1Hex { get; set; }
        public string Combination2Hex { get; set; }
        public string Combination3Hex { get; set; }
        public bool AlreadyPicked { get; set; }

        public void PickFromRgb()
        {
            if (AlreadyPicked) return;

            PickedColor = RgbColor.ToSolidColorBrush();
            PickedColorHex = RgbColor.ToHexString(isAlphaIncluded: false);

            ContrastingColor = ColorProcessor.PickContrastingColor(RgbColor.Red.Value, RgbColor.Green.Value, RgbColor.Blue.Value, Alpha.Value);
        }
        public void PickFromHsv()
        {
            if (AlreadyPicked) return;

            RgbColor tempRgbColor = HsvColor.ToRgbColor();
            UpdateHueBrush();

            PickedColor = HsvColor.ToSolidColorBrush();
            PickedColorHex = HsvColor.ToHexString(isAlphaIncluded: false);

            PickingEllipseColor = PickedColor;

            ContrastingColor = ColorProcessor.PickContrastingColor(tempRgbColor.Red.Value, tempRgbColor.Green.Value, tempRgbColor.Blue.Value, Alpha.Value);
        }
        public void ConvertValuesFromRgb()
        {
            AlreadyPicked = true;

            HsvColor = RgbColor.ToHsvColor();
            HslColor = RgbColor.ToHslColor();
            CmykColor = RgbColor.ToCmykColor();
            PickingEllipseColor = PickedColor;
            UpdateHueBrush();

            AlreadyPicked = false;
        }
        public void ConvertValuesFromHsv()
        {
            AlreadyPicked = true;

            RgbColor = HsvColor.ToRgbColor();
            HslColor = RgbColor.ToHslColor();
            CmykColor = RgbColor.ToCmykColor();
            PickingEllipseColor = PickedColor;

            AlreadyPicked = false;
        }
        public void UpdateHueBrush()
        {
            RgbColor toRgb = HsvColor.MostSaturated.ToRgbColor();
            PickedHue = Color.FromRgb((byte)toRgb.Red.Value, (byte)toRgb.Green.Value, (byte)toRgb.Blue.Value);
        }

        public PickerViewModel()
        {
            Alpha = new EightBitComponent(255);
            RgbColor = new RgbColor(255, 0, 0);
            HsvColor = new HsvColor(360, 100, 100);
            HslColor = new HslColor(0, 100, 50);
            CmykColor = new CmykColor(0, 100, 100, 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
