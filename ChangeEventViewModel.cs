using GiftNotation.Commands.EventCommands;
using GiftNotation.Models;
using GiftNotation.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiftNotation.ViewModels
{
    public class ChangeEventViewModel : ViewModelBase
    {
        private int _eventId;
        private string _eventName;
        private DateTime _date;
        private string _selectedEventTypeName;

        private readonly EventService _eventService;
        private readonly ContactService _contactService;
        private readonly EventViewModel _eventViewModel;
        private DisplayEventModel _selectedEvent;
        private EventType _selectedEventType;

        private AddContactOnEventOnChangeCommand _addContactOnEventCommand;

        private Contact _selectedContact;

        public ObservableCollection<EventType> EventTypes { get; private set; } = new ObservableCollection<EventType>();
        public ObservableCollection<Contact> Contacts { get; private set; } = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> ContactsOnEvent { get; private set; } = new ObservableCollection<Contact>();

        private Contact _selectedContactOnEvent;
        public Contact SelectedContactOnEvent
        {
            get => _selectedContactOnEvent;
            set
            {
                if (SetProperty(ref _selectedContactOnEvent, value))
                {
                    // Обновляем состояние кнопки удаления
                    DeleteContactFromEventOnChangeCommand.RaiseCanExecuteChanged();
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
                    ((AddContactOnEventOnChangeCommand)AddContactOnEventOnChangeCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public DeleteContactFromEventOnChangeCommand DeleteContactFromEventOnChangeCommand { get; }
        public ICommand AddContactOnEventOnChangeCommand => _addContactOnEventCommand;

        public int EventId
        {
            get { return _eventId; }
        }

        public string EventName
        {
            get => _eventName;
            set
            {
                if (SetProperty(ref _eventName, value))
                {
                    // Пример правильного вызова RaiseCanExecuteChanged для конкретной команды
                    ChangeEventCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime EventDate
        {
            get { return _date; }
            set
            {
                SetProperty(ref _date, value);
            }
        }

        public EventType? SelectedEventType
        {
            get { return _selectedEventType; }
            set => SetProperty(ref _selectedEventType, value);
        }

        public string SelectedEventTypeName
        {
            get => _selectedEventTypeName;
            set => SetProperty(ref _selectedEventTypeName, value);
        }

        public ChangeEventCommand ChangeEventCommand { get; set; }

        public ChangeEventViewModel(EventService eventService, EventViewModel eventViewModel, ContactService contactService)
        {
            _contactService = contactService;
            _eventService = eventService;
            _eventViewModel = eventViewModel;
            ChangeEventCommand = new ChangeEventCommand(eventService, this, _eventViewModel);
            DeleteContactFromEventOnChangeCommand = new DeleteContactFromEventOnChangeCommand(this, _contactService);
            _addContactOnEventCommand = new AddContactOnEventOnChangeCommand(this);
            LoadData();
            LoadContactsOnEvent();
            LoadContacts();
            LoadEventTypes();
        }

        private async void LoadData()
        {
            var events = await _eventService.GetDisplayEventModelByID(_eventViewModel.SelectedEvent.EventId);
            foreach (var event_ in events)
            {
                _selectedEvent = event_;
            }
            _eventId = _selectedEvent.EventId;
            _eventName = _selectedEvent.EventName;
            _date = _selectedEvent.EventDate;
            _selectedEventTypeName = _selectedEvent.EventTypeName;

        }

        private async void LoadContacts()
        {
            var contacts = await _contactService.GetAllContactsNotOnEvent(_selectedEvent.EventId);
            Contacts = new ObservableCollection<Contact>(contacts);
        }

        private async void LoadContactsOnEvent()
        {
            var contacts = await _contactService.GetAllContactsOnEvent(_selectedEvent.EventId);
            ContactsOnEvent = new ObservableCollection<Contact>(contacts);
        }

        private async void LoadEventTypes()
        {
            var eventTypes = await _eventService.GetEventTypesAsync();
            EventTypes = new ObservableCollection<EventType>(eventTypes);
        }
    }
}

