namespace ColorPicker.ColorModels.ColorComponents
{
    public class DegreesComponent
    {
        private int _val;

        public int Value
        {
            get {
                if (_val > 360)
                    return 360;
                else if (_val < 0)
                    return 0;
                else
                    return _val; 
            }
            set { _val = value; }
        }

        public DegreesComponent(int initVal)
        {
            Value = initVal;
        }

    }
}
