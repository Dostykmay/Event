using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GiftNotation.Models;
using GiftNotation.Services;
using GiftNotation.ViewModels;

namespace GiftNotation.Commands.EventCommands
{
    public class ChangeEventCommand : ICommand
    {
        private readonly EventService _eventService;
        private readonly ChangeEventViewModel _changeViewModel;
        private readonly EventViewModel _eventViewModel;

        public ChangeEventCommand(EventService eventService, ChangeEventViewModel changeViewModel, EventViewModel eventViewModel)
        {
            _eventService = eventService;
            _changeViewModel = changeViewModel;
            _eventViewModel = eventViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_changeViewModel.EventName);
        }

        public async void Execute(object? parameter)
        {
            var changeEvent = new DisplayEventModel
            {
                EventId = _changeViewModel.EventId,
                EventName = _changeViewModel.EventName,
                EventDate = _changeViewModel.EventDate,
                EventTypeName = _changeViewModel.SelectedEventType?.EventTypeName
            };
            await _eventService.UpdateEventAsync(changeEvent, _changeViewModel);

            _eventViewModel.LoadEvents();

            if (parameter is Window window)
            {
                window.Close();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
