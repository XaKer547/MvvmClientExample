using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmClientExample.Domain.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MvvmClientExample.ViewModels
{
    public class MailVerificationViewModel : ObservableValidator
    {
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        public MailVerificationViewModel(IMailService mailService, IUserService userService)
        {
            _mailService = mailService;
            _userService = userService;

            var random = new Random();

            VerificationCode = random.Next(1000, 9999);

            var userMail = _userService.GetUser(Session.SessionHandler.UserId).Email;

            Task.Run(async () =>
            {
                await _mailService.SendVerificationCode(VerificationCode, userMail);
            });
        }

        [Required]
        public int VerificationInput { get; set; }

        public int VerificationCode { get; private set; }

        public Action OnVerifactionComplete { get; set; }

        public ICommand VerifyCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (VerificationInput != VerificationCode)
                    {
                        MessageBox.Show("Неверный код");
                        return;
                    }

                    _userService.SetUserVerified(Session.SessionHandler.UserId);

                    OnVerifactionComplete.Invoke();
                });
            }
        }
    }
}
