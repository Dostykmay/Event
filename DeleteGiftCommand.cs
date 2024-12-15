using GiftNotation.Services;
using GiftNotation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiftNotation.Commands.GiftCommands
{
    public class DeleteGiftCommand : ICommand
    {
        private readonly GiftViewModel _viewModel;
        private readonly GiftService _giftService;

        public DeleteGiftCommand(GiftViewModel viewModel, GiftService giftService)
        {
            _viewModel = viewModel;
            _giftService = giftService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedGift != null;
        }

        public async void Execute(object? parameter)
        {
            if (_viewModel.SelectedGift == null) return;

            try
            {
                // Удаление подарка
                await _giftService.DeleteGiftAsync(_viewModel.SelectedGift.GiftId);

                // Удаление из коллекции и сброс выделения
                _viewModel.Gifts.Remove(_viewModel.SelectedGift);
                _viewModel.SelectedGift = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении подарка: {ex.Message}");
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
