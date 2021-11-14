using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPicker.ColorModels.ColorComponents
{
    public class PercentComponent
    {
        private int _val;

        public int Value
        {
            get
            {
                if (_val > 100)
                    return 100;
                else if (_val < 0)
                    return 0;
                else
                    return _val;
            }
            set { _val = value; }
        }

        public PercentComponent(int initVal)
        {
            Value = initVal;
        }
    }
}
