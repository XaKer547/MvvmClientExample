using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;

namespace MvvmClientExample.ViewModels.Abstract
{
    public class NavigationViewModel : ObservableObject
    {
        private protected readonly Frame _navigation;
        public NavigationViewModel(Frame navigation)
        {
            _navigation = navigation;
        }
    }
}
