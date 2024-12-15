using GiftNotation.Services;
using GiftNotation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiftNotation.Commands.EventCommands
{
    public class DeleteEventCommand : ICommand
    {
        private readonly EventService _eventService;
        private readonly EventViewModel _eventViewModel;

        public DeleteEventCommand(EventViewModel eventViewModel, EventService eventService)
        {
            _eventService = eventService;
            _eventViewModel = eventViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _eventViewModel.SelectedEvent != null;
        }

        public async void Execute(object? parameter)
        {
            if (_eventViewModel.SelectedEvent == null) return;

            try
            {
                await _eventService.DeleteEventAsync(_eventViewModel.SelectedEvent.EventId);

                _eventViewModel.Events.Remove(_eventViewModel.SelectedEvent);
                _eventViewModel.SelectedEvent = null;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Ошибка при удалении события: {ex.Message}");
            }
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
