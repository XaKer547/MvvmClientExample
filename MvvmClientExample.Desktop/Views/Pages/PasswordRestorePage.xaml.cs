using CommunityToolkit.Mvvm.DependencyInjection;
using System.Windows.Controls;

namespace MvvmClientExample.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PasswordRestorePage.xaml
    /// </summary>
    public partial class PasswordRestorePage : Page
    {
        public PasswordRestorePage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<ViewModels.PasswordRestoreViewModel>();
        }
    }
}
