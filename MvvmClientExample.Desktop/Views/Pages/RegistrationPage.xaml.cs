using CommunityToolkit.Mvvm.DependencyInjection;
using MvvmClientExample.ViewModels;
using System;
using System.Windows.Controls;

namespace MvvmClientExample.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage(Action close)
        {
            InitializeComponent();
            var viewModel = Ioc.Default.GetRequiredService<RegistrationViewModel>();

            viewModel.CloseWindow = close;

            DataContext = viewModel;
        }
    }
}
