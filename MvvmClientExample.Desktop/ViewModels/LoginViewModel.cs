using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmClientExample.Domain.Models;
using MvvmClientExample.Domain.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;

namespace MvvmClientExample.ViewModels
{
    public class LoginViewModel : ObservableValidator
    {
        private readonly IAuthorizationService _authorizationService;
        public LoginViewModel(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
                ValidateProperty(value, nameof(Login));
            }
        }
        private string _login;

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ValidateProperty(value, nameof(Password));
            }
        }
        private string _password;

        public Action CloseWindow { get; set; }
        public Action OpenVerification { get; set; }
        public ICommand AuthorizeCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    ValidateAllProperties();

                    if (HasErrors)
                        return;

                    var result = _authorizationService.Authorize(new LoginDTO()
                    {
                        Login = Login,
                        Password = Password
                    });

                    if (!result.IsSuccess)
                    {
                        MessageBox.Show("Такого пользователя не существует!");
                        return;
                    }

                    Session.SessionHandler.UserId = result.UserId;

                    if (result.IsVerified)
                    {
                        OpenVerification.Invoke();
                    }
                    else
                    {
                        new MainWindow().Show();

                        CloseWindow?.Invoke();
                    }
                });
            }
        }
    }
}