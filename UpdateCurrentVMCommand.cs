using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GiftNotation.Services;
using GiftNotation.State.Navigators;
using GiftNotation.ViewModels;
using GiftNotation.ViewModels.Factories;

namespace GiftNotation.Commands
{
    public class UpdateCurrentVMCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IGiftNotationViewModelAbstractFactory _viewModelFactory;
        

        // Конструктор команды, принимающий INavigator и IMyFriendsService через DI
        public UpdateCurrentVMCommand(INavigator navigator, IGiftNotationViewModelAbstractFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            
        }


        // Команда доступна для выполнения всегда
        public bool CanExecute(object? parameter) => true;

        // Выполняется при нажатии на кнопку в панели управления
        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
                
            }
        }

        
    }
}
