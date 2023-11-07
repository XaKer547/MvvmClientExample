using MvvmClientExample.ViewModels;
using MvvmClientExample.Views.Pages;
using System.Windows;

namespace MvvmClientExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(_navigation);
        }
    }
}
