using CommunityToolkit.Mvvm.Input;
using MvvmClientExample.ViewModels.Abstract;
using MvvmClientExample.Views.Pages;
using System.Windows.Controls;
using System.Windows.Input;

namespace MvvmClientExample.ViewModels
{
    public class MainViewModel : NavigationViewModel
    {
        public MainViewModel(Frame navigation) : base(navigation)
        { }

        public ICommand OpenOptionsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigation.Navigate(new OptionsPage());
                });
            }
        }

    }
}
