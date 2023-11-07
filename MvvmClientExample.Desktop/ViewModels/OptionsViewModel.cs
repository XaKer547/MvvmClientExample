using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmClientExample.Application.Helpers;
using MvvmClientExample.Domain.Services;
using MvvmClientExample.Views;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace MvvmClientExample.ViewModels
{
    public class OptionsViewModel : ObservableValidator
    {
        public string RealPassword { get; init; }
        private readonly IUserService _userService;
        public OptionsViewModel(IUserService userService)
        {
            _userService = userService;
            var user = _userService.GetUser(Session.SessionHandler.UserId);

            RealPassword = user.Password;

            if (user.IsVerified)
            {
                TwoFactorStatus = "Включена";
                ChangeTwoFactorStatus = new RelayCommand(TurnOffTwoFactor);
            }
            else
            {
                TwoFactorStatus = "Выключена";
                ChangeTwoFactorStatus = new RelayCommand(TurnOnTwoFactor);
            }
        }

        public string TwoFactorStatus
        {
            get => _twoFactorStatus;
            set
            {
                _twoFactorStatus = value;
                OnPropertyChanged(nameof(TwoFactorStatus));
            }
        }
        private string _twoFactorStatus;

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Compare(nameof(RealPassword), ErrorMessage = "Это не старый пароль")]
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
                ValidateProperty(value.ToHash(), nameof(OldPassword));
            }
        }
        private string _oldPassword;

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string NewPassword
        {
            get => _newPassword;

            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
                ValidateProperty(value, nameof(NewPassword));
            }
        }
        private string _newPassword;

        public ICommand ChangePasswordCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    ValidateProperty(OldPassword.ToHash(), nameof(OldPassword));

                    ValidateProperty(NewPassword, nameof(NewPassword));

                    if (HasErrors)
                        return;

                    await _userService.ChangePassword(Session.SessionHandler.UserId, NewPassword);
                });
            }
        }


        public ICommand ChangeTwoFactorStatus
        {
            get => _changeTwoFactorStatus;
            set
            {
                _changeTwoFactorStatus = value;
                OnPropertyChanged(nameof(ChangeTwoFactorStatus));
            }
        }
        private ICommand _changeTwoFactorStatus;


        private void TurnOnTwoFactor()
        {
            var verificationWindow = new VerificationWindow().ShowDialog();

            if (verificationWindow != true)
                return;

            _userService.SetUserVerified(Session.SessionHandler.UserId);
            TwoFactorStatus = "Включена";

            ChangeTwoFactorStatus = new RelayCommand(TurnOffTwoFactor);
        }
        private void TurnOffTwoFactor()
        {
            _userService.DropTwoFactorAuthentification(Session.SessionHandler.UserId);
            TwoFactorStatus = "Выключена";

            ChangeTwoFactorStatus = new RelayCommand(TurnOnTwoFactor);
        }
    }
}
