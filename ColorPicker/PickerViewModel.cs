using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ColorPicker
{
    public class PickerViewModel : INotifyPropertyChanged
    {
        public SolidColorBrush PickedColor { get; set; } = new SolidColorBrush(Color.FromArgb(255,0,0,0));

        private string _pickedColorHex;
        public string PickedColorHex
        {
            get { 
                return "#"+AlphaValue.ToString("X2") + RedValue.ToString("X2") + GreenValue.ToString("X2") + BlueValue.ToString("X2"); 
            }
            set { _pickedColorHex = value; }
        }


        public int RedValue { get; set; } = 0;
        public int GreenValue { get; set; } = 0;
        public int BlueValue { get; set; } = 0;
        public int AlphaValue { get; set; } = 255;

        public void PickNewColor()
        {
            PickedColor = new SolidColorBrush(Color.FromArgb((byte)AlphaValue, (byte)RedValue, (byte)GreenValue, (byte)BlueValue));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
