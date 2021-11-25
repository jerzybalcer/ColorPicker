using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ColorPicker.UserControls
{
    /// <summary>
    /// Interaction logic for HsvPicker.xaml
    /// </summary>
    public partial class HsvPicker : UserControl
    {
        private Picker _picker;

        public HsvPicker()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _picker = (Picker)DataContext;
        }

        private void HueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _picker.PickFromHsv();
        }

        private void HueSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _picker.ConvertValuesFromHsv();
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(Canvas);

                double newPosX = p.X - PickingEllipse.ActualWidth / 2;
                double newPosY = p.Y - PickingEllipse.ActualHeight / 2;

                Canvas.SetLeft(PickingEllipse, newPosX);
                Canvas.SetTop(PickingEllipse, Canvas.ActualHeight - newPosY);

                PickingEllipse.CaptureMouse();
                _picker.PickFromHsv();
                // todo: change copied/uncopied
            }
        }

        private void PickingEllipse_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PickingEllipse.ReleaseMouseCapture();
            _picker.ConvertValuesFromHsv();
        }
    }
}
