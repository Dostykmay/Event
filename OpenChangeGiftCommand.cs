using GiftNotation.Services;
using GiftNotation.ViewModels;
using GiftNotation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiftNotation.Commands.GiftCommands
{
    public class OpenChangeGiftCommand : ICommand
    {
        private readonly GiftService _giftService;
        private readonly ContactService _contactService;
        private readonly EventService _eventService;
        private readonly GiftViewModel _viewModel;

        public OpenChangeGiftCommand(GiftViewModel viewModel, GiftService giftService, ContactService contactService, EventService eventService)
        {
            _giftService = giftService;
            _viewModel = viewModel;
            _contactService = contactService;
            _eventService = eventService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedGift != null;
        }

        public void Execute(object? parameter)
        {
            var changeGiftViewModel = new ChangeGiftViewModel(_viewModel, _giftService, _contactService, _eventService);
            var changeGiftWindow = new ChangingGifts
            {
                DataContext = changeGiftViewModel
            };

            changeGiftWindow.ShowDialog();
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
