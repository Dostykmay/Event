using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GiftNotation.ViewModels;

namespace GiftNotation.State.Navigators
{
    //Перечисление страниц
    public enum ViewType
    {
        Calendar,
        Contacts,
        Events,
        Gifts
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentVMCommand { get; }
    }
}
