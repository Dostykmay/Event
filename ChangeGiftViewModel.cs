using GiftNotation.Models;
using GiftNotation.Services;
using GiftNotation.Commands.GiftCommands;
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
    public class ChangeGiftViewModel : ViewModelBase
    {
        private readonly GiftViewModel _giftViewModel;
        private readonly GiftService _giftService;
        private readonly ContactService _contactService;
        private readonly EventService _eventService;

        private int _giftId;
        private string _giftName;
        private string _giftDescription;
        private string _url;
        private double _price;
        private string _giftPic;
        private int? _selectedContactId;
        private int? _contactId;
        private string _contactName;
        private int? _selectedEventId;
        private int? _eventId;
        private string _eventName;
        private string _statusName;

        internal DisplayGiftModel _selectedGift;
        private Status? _selectedStatus;
        private Contact? _selectedContact;
        private Event? _selectedEvent;

        public ObservableCollection<Status> Statuses { get; private set; } = new ObservableCollection<Status>();
        public ObservableCollection<Contact> Contacts { get; private set; } = new ObservableCollection<Contact>();
        public ObservableCollection<Event> Events { get; private set; } = new ObservableCollection<Event>();

        public int GiftId { get => _giftId; }

        public string GiftName
        {
            get => _giftName;
            set => SetProperty(ref _giftName, value);
        }

        public string Description
        {
            get => _giftDescription;
            set => SetProperty(ref _giftDescription, value);
        }

        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public double Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public string GiftPic
        {
            get => _giftPic;
            set => SetProperty(ref _giftPic, value);
        }

        public int? SelectedContactId
        {
            get => _selectedContactId = SelectedContact?.ContactId;
            set => SetProperty(ref _selectedContactId, value);
        }

        public int? ContactId
        {
            get => _contactId;
            set => SetProperty(ref _contactId, value);
        }

        public string ContactName
        {
            get => _contactName;
            set => SetProperty(ref _contactName, value);
        }
        public int? SelectedEventId
        {
            get => _selectedEventId = SelectedEvent?.EventId;
            set => SetProperty(ref _selectedEventId, value);
        }

        public int? EventId
        {
            get => _eventId;
            set => SetProperty(ref _eventId, value);
        }

        public string EventName
        {
            get => _eventName;
            set => SetProperty(ref _eventName, value);
        }

        public string StatusName
        {
            get => _statusName;
            set => SetProperty(ref _statusName, value);
        }


        public Status? SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                SetProperty(ref _selectedStatus, value);
            }
        }

        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }

        public Event? SelectedEvent
        {
            get => _selectedEvent;
            set => SetProperty(ref _selectedEvent, value);
        }

        public ICommand ChangeGiftCommand { get; }

        public ChangeGiftViewModel(GiftViewModel giftViewModel, GiftService giftService, ContactService contactService, EventService eventService)
        {
            _giftViewModel = giftViewModel;
            _giftService = giftService;
            _eventService = eventService;
            _contactService = contactService;
            LoadData();
            LoadContacts();
            LoadEvents();
            LoadStatuses();
            ChangeGiftCommand = new ChangeGiftCommand(giftService, giftViewModel, this);
        }

        private async void LoadStatuses()
        {
            var statuses = await _giftService.GetAllStatuses();
            foreach (var status in statuses)
            {
                Statuses.Add(status);
            }
        }

        private async void LoadContacts()
        {
            var contacts = await _contactService.GetAllContacts();
            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }
        }
        private async void LoadEvents()
        {
            var events = await _eventService.GetAllEvents();
            foreach (var event_ in events)
            {
                Events.Add(event_);
            }
        }

        private async void LoadData()
        {
            var gifts = await _giftService.GetDisplayGiftModelByID(_giftViewModel.SelectedGift.GiftId);
            foreach (var gift in gifts) { 
                _selectedGift = gift;
            }
            _giftId = _selectedGift.GiftId;
            _giftName = _selectedGift.GiftName;
            _giftDescription = _selectedGift.Description;
            _url = _selectedGift.Url;
            _price = _selectedGift.Price;
            _giftPic = _selectedGift.GiftPic;
            _contactId = _selectedGift.ContactId;
            _contactName = _selectedGift.ContactName;
            _eventId = _selectedGift.EventId;
            _eventName = _selectedGift.EventName;
            _statusName = _selectedGift.StatusName;

        }

    }
}
