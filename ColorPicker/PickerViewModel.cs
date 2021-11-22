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
        public bool AlreadyPicked { get; set; }

        public void PickFromRgb()
        {
            if (AlreadyPicked) return;

            PickedColor = new SolidColorBrush(
                Color.FromArgb((byte)Alpha.Value, (byte)RgbColor.Red.Value, (byte)RgbColor.Green.Value, (byte)RgbColor.Blue.Value));

            ContrastingColor = ColorProcessor.PickContrastingColor(RgbColor.Red.Value, RgbColor.Green.Value, RgbColor.Blue.Value, Alpha.Value);
            PickedColorHex = "#" + Alpha.Value.ToString("X2") + RgbColor.Red.Value.ToString("X2") + 
                RgbColor.Green.Value.ToString("X2") + RgbColor.Blue.Value.ToString("X2");
        }
        public void PickFromHsv()
        {
            if (AlreadyPicked) return;

            RgbColor tempRgbColor = HsvColor.ToRgbColor();
            UpdateHueBrush();

            PickedColor = new SolidColorBrush(
                Color.FromArgb((byte)Alpha.Value, (byte)tempRgbColor.Red.Value, (byte)tempRgbColor.Green.Value, (byte)tempRgbColor.Blue.Value));
            PickingEllipseColor = PickedColor;

            ContrastingColor = ColorProcessor.PickContrastingColor(tempRgbColor.Red.Value, tempRgbColor.Green.Value, tempRgbColor.Blue.Value, Alpha.Value);
            PickedColorHex = "#" + Alpha.Value.ToString("X2") + tempRgbColor.Red.Value.ToString("X2") +
                tempRgbColor.Green.Value.ToString("X2") + tempRgbColor.Blue.Value.ToString("X2");
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
            RgbColor convertedHsv = HsvColor.MostSaturated.ToRgbColor();
            PickedHue = Color.FromRgb((byte)convertedHsv.Red.Value, (byte)convertedHsv.Green.Value, (byte)convertedHsv.Blue.Value);
        }

        public PickerViewModel()
        {
            Alpha = new EightBitComponent(255);
            RgbColor = new RgbColor(255, 0, 0);
            HsvColor = new HsvColor(360, 100, 100);
            HslColor = new HslColor(0, 100, 0);
            CmykColor = new CmykColor(0, 0, 0, 100);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
