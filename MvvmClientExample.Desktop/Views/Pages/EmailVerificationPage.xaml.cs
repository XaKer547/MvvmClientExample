using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Windows.Controls;

namespace MvvmClientExample.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmailVerificationPage.xaml
    /// </summary>
    public partial class EmailVerificationPage : Page
    {
        public EmailVerificationPage(Action onVerificationComplete)
        {
            InitializeComponent();

            var viewmodel = Ioc.Default.GetRequiredService<ViewModels.MailVerificationViewModel>();

            viewmodel.OnVerifactionComplete = onVerificationComplete;

            DataContext = viewmodel;
        }
    }
}
