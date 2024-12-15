using GiftNotation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiftNotation.Commands.ContactCommands
{
    public class DeleteGiftForContactOnAddCommand : ICommand
    {
        private readonly AddContactViewModel _viewModel;

        public DeleteGiftForContactOnAddCommand(AddContactViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            // Убедимся, что выбран контакт на событии
            return _viewModel.SelectedGiftForContact != null;
        }

        public async void Execute(object? parameter)
        {
            await DeleteContactFromEventAsync();
        }

        private async Task DeleteContactFromEventAsync()
        {
            var contact = _viewModel.SelectedGiftForContact;
            if (contact != null)
            {
                // Обновляем списки
                _viewModel.GiftsForContact.Remove(contact);  // Удаление из списка контактов на событии
                _viewModel.Gifts.Add(contact); // Добавление обратно в общий список контактов

            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
