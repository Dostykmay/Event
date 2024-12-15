using GiftNotation.Commands.EventCommands;
using GiftNotation.Models;
using GiftNotation.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace GiftNotation.ViewModels
{
    public class AddEventViewModel : ViewModelBase
    {
        private readonly EventService _eventService;
        private readonly ContactService _contactService;
        private readonly EventViewModel _eventViewModel;

        private int _eventId;
        private string _eventName;
        private DateTime _date;
        private EventType _eventType;
        private Contact _selectedContact;
        private AddContactOnEventOnAddCommand _addContactOnEventCommand;

        public ObservableCollection<EventType> EventTypes { get; private set; } = new ObservableCollection<EventType>();
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> ContactsOnEvent { get; set; } = new ObservableCollection<Contact>();

        private Contact _selectedContactOnEvent;
        public Contact SelectedContactOnEvent
        {
            get => _selectedContactOnEvent;
            set
            {
                if (SetProperty(ref _selectedContactOnEvent, value))
                {
                    // Обновляем состояние кнопки удаления
                    DeleteContactFromEventCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                if (SetProperty(ref _selectedContact, value))
                {
                    // Уведомляем команду, что условие для CanExecute могло измениться
                    AddContactOnEventCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string EventName
        {
            get => _eventName;
            set
            {
                if (SetProperty(ref _eventName, value))
                {
                    // Пример правильного вызова RaiseCanExecuteChanged для конкретной команды
                    AddEventCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime EventDate
        {
            get { return _date; }
            set => SetProperty(ref _date, value);
        }

        public EventType EventType
        {
            get { return _eventType; }
            set => SetProperty(ref _eventType, value);
        }

        public DeleteContactFromEventOnAddCommand DeleteContactFromEventCommand { get; }
        public AddEventCommand AddEventCommand { get; }
        public AddContactOnEventOnAddCommand AddContactOnEventCommand => _addContactOnEventCommand; // Команда доступна в ViewModel


        public AddEventViewModel(EventService eventService, EventViewModel eventViewModel, ContactService contactService)
        {
            _eventService = eventService;
            _contactService = contactService;
            _eventViewModel = eventViewModel;
            AddEventCommand = new AddEventCommand(eventService, this, _eventViewModel);
            _addContactOnEventCommand = new AddContactOnEventOnAddCommand(this);
            DeleteContactFromEventCommand = new DeleteContactFromEventOnAddCommand(this);
            LoadContacts();
            LoadEventTypes();
        }

        private async void LoadContacts()
        {
            var contacts = await _contactService.GetAllContacts();
            Contacts = new ObservableCollection<Contact>(contacts);
        }

        private async void LoadEventTypes()
        {
            var eventTypes = await _eventService.GetEventTypesAsync();
            EventTypes = new ObservableCollection<EventType>(eventTypes);
        }

    }
}
