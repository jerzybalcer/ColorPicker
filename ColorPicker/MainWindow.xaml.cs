using System.Windows;

namespace ColorPicker
{
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
