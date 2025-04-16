using FuckingDo.Services.Contracts;
using Wpf.Ui;
using FuckingDo.ViewModels.Windows;

namespace FuckingDo.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IWindow
    {
        public MainWindowViewModel ViewModel { get; }


        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationService navigationService)
        {
            Wpf.Ui.Appearance.SystemThemeWatcher.Watch(this);

            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            navigationService.SetNavigationControl(NavigationView);

        }
    }
}