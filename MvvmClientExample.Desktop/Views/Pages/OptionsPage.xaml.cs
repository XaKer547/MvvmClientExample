using CommunityToolkit.Mvvm.DependencyInjection;
using MvvmClientExample.ViewModels;
using System.Windows.Controls;

namespace MvvmClientExample.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для OptionsPage.xaml
    /// </summary>
    public partial class OptionsPage : Page
    {
        public OptionsPage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<OptionsViewModel>();
        }
    }
}
