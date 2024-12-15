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

namespace GiftNotation.Commands.ContactCommands
{
    public class AddContactCommand : ICommand
    {
        private readonly ContactService _contactService;
        private readonly EventService _eventService;
        private readonly ContactViewModel _contactViewModel;
        private readonly AddContactViewModel _addContactViewModel;

        public AddContactCommand(ContactService contactService, ContactViewModel contactViewModel, AddContactViewModel addContactViewModel)
        {
            _contactService = contactService;
            _contactViewModel = contactViewModel;
            _addContactViewModel = addContactViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            var newContact = new DisplayContactModel
            {
                ContactName = _addContactViewModel.ContactName ?? string.Empty,
                Bday = _addContactViewModel.Bday,
                RelpTypeName = _addContactViewModel.SelectedRelpType?.RelpTypeName ?? string.Empty,
            };

            await _contactService.AddContactAsync(newContact);

            // Обновляем список подарков после добавления
            _contactViewModel.LoadContacts();


            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}
