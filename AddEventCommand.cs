using GiftNotation.Models;
using GiftNotation.Services;
using GiftNotation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GiftNotation.Commands.EventCommands
{
    public class AddEventCommand : ICommand
    {
        private readonly EventService _eventService;
        private readonly AddEventViewModel _addViewModel;
        private readonly EventViewModel _eventViewModel;

        public AddEventCommand(EventService eventService, AddEventViewModel addEventViewModel, EventViewModel eventViewModel)
        {
            _eventService = eventService;
            _addViewModel = addEventViewModel;
            _eventViewModel = eventViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_addViewModel.EventName);  // Исправлено условие
        }

        public async void Execute(object? parameter)
        {
            var newContact = new DisplayEventModel
            {
                EventName = _addViewModel.EventName,
                EventDate = _addViewModel.EventDate,
                EventTypeName = _addViewModel.EventType?.EventTypeName ?? string.Empty,
            };

            await _eventService.AddEventAsync(newContact, _addViewModel);
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
