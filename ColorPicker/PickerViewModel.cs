using System;
using System.ComponentModel;
using System.Windows.Media;

namespace ColorPicker
{
    public class PickerViewModel : INotifyPropertyChanged
    {
        public SolidColorBrush PickedColorBrush { get; set; } = new SolidColorBrush(Color.FromArgb(255,0,0,0));
        public string PickedColorHex
        {
            get => "#" + AlphaValue.ToString("X2") + RedValue.ToString("X2") + GreenValue.ToString("X2") + BlueValue.ToString("X2");
        }
        public SolidColorBrush PickedHexTextColor
        {
            get
            {
                double brightness = Math.Sqrt(RedValue*RedValue*0.241 + GreenValue*GreenValue*0.691 + BlueValue*BlueValue*0.068);
                if (brightness > 130 || AlphaValue < 110) return new SolidColorBrush(Colors.Black);
                else return new SolidColorBrush(Colors.White);
            }
        }

        public int RedValue { get; set; } = 0;
        public int GreenValue { get; set; } = 0;
        public int BlueValue { get; set; } = 0;
        public int AlphaValue { get; set; } = 255;

        public void PickNewColor()
        {
            PickedColorBrush = new SolidColorBrush(Color.FromArgb((byte)AlphaValue, (byte)RedValue, (byte)GreenValue, (byte)BlueValue));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
