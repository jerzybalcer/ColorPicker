using System.ComponentModel;

namespace ColorPicker
{
    public class ColorComponent : INotifyPropertyChanged
    {
        private int _val;
        public int Value
        {
            get {
                if (_val > 255) return 255;
                else if (_val < 0) return 0;
                else return _val;
            }
            set { _val = value;}
        }

        public ColorComponent(int initVal)
        {
            Value = initVal;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
