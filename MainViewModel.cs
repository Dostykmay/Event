using GiftNotation.Commands;
using GiftNotation.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiftNotation.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        
        public ICommand UpdateCurrentVMCommand { get; }

        public MainViewModel(INavigator navigator, UpdateCurrentVMCommand updateCurrentVMCommand)
        {
            Navigator = navigator;
            UpdateCurrentVMCommand = navigator.UpdateCurrentVMCommand;
            UpdateCurrentVMCommand.Execute(ViewType.Calendar);
        }
    }
}
