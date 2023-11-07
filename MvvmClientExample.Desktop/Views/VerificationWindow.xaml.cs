using MvvmClientExample.Views.Pages;
using System.Windows;

namespace MvvmClientExample.Views
{
    /// <summary>
    /// Логика взаимодействия для VerificationWindow.xaml
    /// </summary>
    public partial class VerificationWindow : Window
    {
        public VerificationWindow()
        {
            InitializeComponent();

            _navigation.Navigate(new EmailVerificationPage(() => DialogResult = true));
        }
    }
}
