using GiftNotation.State.Navigators;
using GiftNotation.ViewModels;
using GiftNotation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GiftNotation.Commands
{
    public class OpenCloseFilterCommand : ICommand
    {
        private readonly EventViewModel _eventViewModel;

        public OpenCloseFilterCommand(EventViewModel eventViewModel)
        {
            _eventViewModel = eventViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            // Переключение видимости окна фильтров
            _eventViewModel.ToggleFiltersWindow();
        }
    }


}
