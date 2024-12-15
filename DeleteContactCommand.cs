using GiftNotation.Services;
using GiftNotation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiftNotation.Commands.ContactCommands
{
    public class DeleteContactCommand : ICommand
    {
        private readonly ContactViewModel _viewModel;
        private readonly ContactService _contactService;

        public DeleteContactCommand(ContactViewModel viewModel, ContactService contactService)
        {
            _viewModel = viewModel;
            _contactService = contactService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedContact != null;
        }

        public async void Execute(object? parameter)
        {
            if (_viewModel.SelectedContact == null) return;

                // Удаление подарка
                await _contactService.DeleteContactAsync(_viewModel.SelectedContact.ContactId);

                // Удаление из коллекции и сброс выделения
                _viewModel.Contacts.Remove(_viewModel.SelectedContact);
                _viewModel.SelectedContact = null;
            
            
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
