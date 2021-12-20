using ColorPicker.PickedColors;
using ColorPicker.HueLightsUtilities;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ColorPicker
{
    public partial class MainWindow : Window
    {
        private Picker _picker;
        private int _complements = 4;
        private bool hueLightsWindowOpen = false;
        private HueLightsConfigWindow hueLightsWindow;

        public MainWindow()
        {
            InitializeComponent();
            _picker = new Picker();
            DataContext = _picker;
        }

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _picker.PickFromRgb();
        }
        private void ColorSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _picker.ConvertValuesFromRgb();
        }

        private void RgbTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            _picker.ConvertValuesFromRgb();
        }

        private void HsvTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            _picker.PickFromHsv();
            _picker.ConvertValuesFromHsv();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_complements == 6)
            {
                _complements = 0;

                ColorTiles.ColumnDefinitions[2].Width = new GridLength(0);
                ColorTiles.ColumnDefinitions[1].Width = new GridLength(0);
            }
            else if(_complements == 3)
            {
                _complements = 4;

                ColorTiles.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
            }
            else if(_complements == 0)
            {
                _complements = 1;

                ColorTiles.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                _complements++;
            }

            _picker.Combination = (CombinationType)_complements;
            _picker.UpdateComplements();
        }

        private void HueLightsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!hueLightsWindowOpen)
            {
                hueLightsWindow = new HueLightsConfigWindow();
                hueLightsWindow.Show();
                hueLightsWindowOpen = true;

                hueLightsWindow.Unloaded += (s, e) => { hueLightsWindowOpen = false; };
            }
            else
            {
                hueLightsWindow.Focus();
            }
        }
    }
}
