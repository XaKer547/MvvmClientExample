using CommunityToolkit.Mvvm.Input;
using MvvmClientExample.ViewModels.Abstract;
using MvvmClientExample.Views.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MvvmClientExample.ViewModels
{
    public class AuthorizationViewModel : NavigationViewModel
    {
        public AuthorizationViewModel(Frame navigation) : base(navigation)
        {
            NavigationCommand = new RelayCommand(LoginNavigate);
            NavigationCommand.Execute(this);
        }

        public string NavigationOffer
        {
            get => _navigationOffer;

            set
            {
                _navigationOffer = value;
                OnPropertyChanged(nameof(NavigationOffer));
            }
        }
        private string _navigationOffer;

        public ICommand NavigationCommand
        {
            get => _navigationCommand;
            set
            {
                _navigationCommand = value;
                OnPropertyChanged(nameof(NavigationCommand));
            }
        }
        public ICommand _navigationCommand;


        public Visibility RestorePasswordButtonVisibility
        {
            get => _restorePasswordVisibility;
            set
            {
                _restorePasswordVisibility = value;
                OnPropertyChanged(nameof(RestorePasswordButtonVisibility));
            }
        }
        private Visibility _restorePasswordVisibility;

        public ICommand OpenRestorePasswordCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigation.Navigate(new PasswordRestorePage());
                    NavigationCommand = new RelayCommand(LoginNavigate);
                    RestorePasswordButtonVisibility = Visibility.Collapsed;
                    NavigationOffer = "Назад к авторизации";
                });
            }
        }

        public ICommand OpenVerificationCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigation.Navigate(new EmailVerificationPage(() =>
                    {
                        new MainWindow().Show();
                        CloseWindow.Invoke();
                    }));

                    RestorePasswordButtonVisibility = Visibility.Collapsed;
                });
            }
        }

        public Action CloseWindow { get; set; }
        private void LoginNavigate()
        {
            _navigation.Navigate(new LoginPage(CloseWindow, () =>
            {
                _navigation.Navigate(new EmailVerificationPage(() =>
                {
                    new MainWindow().Show();
                    CloseWindow.Invoke();
                }));

                RestorePasswordButtonVisibility = Visibility.Collapsed;
            }));

            NavigationCommand = new RelayCommand(RegistrationNavigate);
            RestorePasswordButtonVisibility = Visibility.Visible;
            NavigationOffer = "Я здесь впервые";
        }
        private void RegistrationNavigate()
        {
            _navigation.Navigate(new RegistrationPage(CloseWindow));
            NavigationCommand = new RelayCommand(LoginNavigate);
            RestorePasswordButtonVisibility = Visibility.Collapsed;
            NavigationOffer = "У меня есть аккаунт";
        }

    }
}
