using CommunityToolkit.Mvvm.DependencyInjection;
using MvvmClientExample.ViewModels;
using System;
using System.Windows.Controls;

namespace MvvmClientExample.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(Action close, Action verification)
        {
            InitializeComponent();
            var viewModel = Ioc.Default.GetRequiredService<LoginViewModel>();

            viewModel.OpenVerification = verification;
            viewModel.CloseWindow = close;

            DataContext = viewModel;

            //    var db = Ioc.Default.GetRequiredService<AuthDbContext>();
            //    db.Database.EnsureDeleted();
            //    db.Database.EnsureCreated();
        }
    }
}
