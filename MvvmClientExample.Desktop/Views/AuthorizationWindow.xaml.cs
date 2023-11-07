using System.Windows;

namespace MvvmClientExample.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.AuthorizationViewModel(_navigation)
            {
                CloseWindow = Close
            };
        }
    }
}
