using System.Diagnostics;
using System.Windows;

namespace ColorPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PickerViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new PickerViewModel();
            DataContext = _viewModel;
        }

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _viewModel.PickNewColor();
        }
    }
}
