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
    public class RegistrationViewModel : ObservableValidator
    {
        private readonly IAuthorizationService _authorizationService;
        public RegistrationViewModel(IAuthorizationService authorizationService)
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
        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ValidateProperty(value, nameof(Password));
            }
        }
        private string? _password;

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string? RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                _repeatPassword = value;
                OnPropertyChanged(nameof(RepeatPassword));
                ValidateProperty(value, nameof(RepeatPassword));
            }
        }
        private string? _repeatPassword;

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Не корректный формат")]
        public string Email
        {
            get => _email;

            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                ValidateProperty(value, nameof(Email));
            }
        }
        private string _email;

        public Action CloseWindow { get; set; }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    ValidateAllProperties();

                    if (HasErrors)
                        return;

                    await _authorizationService.Register(new RegisterDTO()
                    {
                        Login = Login,
                        Password = Password,
                        Email = Email
                    });

                    var result = _authorizationService.Authorize(new LoginDTO()
                    {
                        Login = Login,
                        Password = Password,
                    });

                    Session.SessionHandler.UserId = result.UserId;

                    if (!result.IsSuccess)
                    {
                        MessageBox.Show("Не удалось зарегистрировать пользователя");
                        return;
                    };

                    new MainWindow().Show();

                    CloseWindow?.Invoke();
                });
            }
        }
    }
}
