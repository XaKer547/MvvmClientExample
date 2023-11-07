using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmClientExample.Domain.Models.Enums;
using MvvmClientExample.Domain.Services;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;

namespace MvvmClientExample.ViewModels
{
    public class PasswordRestoreViewModel : ObservableValidator
    {
        private readonly IAuthorizationService _authorizationService;
        public PasswordRestoreViewModel(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
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

        public string Password
        {
            get => _password;
            set
            {
                _password = value;

                OnPropertyChanged(nameof(Password));
            }
        }
        private string _password;

        public ICommand RestorePasswordCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    ValidateAllProperties();

                    if (HasErrors)
                        return;

                    var request = await _authorizationService.BeginPasswordRestore(Email);

                    if (request.Result == PasswordRestoreResults.AlreadyUsed)
                    {
                        Password = "Вы уже использовали временный пароль";
                        return;
                    }
                    else if (request.Result == PasswordRestoreResults.EmailNotFound)
                    {
                        Password = "Почта не найдена";
                        return;
                    }

                    Clipboard.SetText(request.TemporaryPassword);

                    MessageBox.Show("Пароль скопирован в буфер обмена");

                    Password = request.TemporaryPassword;
                });
            }
        }
    }
}
