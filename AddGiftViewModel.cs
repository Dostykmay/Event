using GiftNotation.Models;
using GiftNotation.Services;
using GiftNotation.Commands.GiftCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiftNotation.ViewModels
{
    public class AddGiftViewModel : ViewModelBase
    {
        private readonly GiftService _giftService;
        private readonly EventService _eventService;
        private readonly ContactService _contactService;

        private string _giftName;
        private string _giftDescription;
        private string _url;
        private double _price;
        private string _giftPic;
        
        private Status? _selectedStatus;
        private Contact? _selectedContact;
        private Event? _selectedEvent;

        public ObservableCollection<Status> Statuses { get; private set; } = new ObservableCollection<Status>();
        public ObservableCollection<Contact> Contacts { get; private set; } = new ObservableCollection<Contact>();
        public ObservableCollection<Event> Events { get; private set; } = new ObservableCollection<Event>();


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

        public Status? SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
            
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
        

        public ICommand AddGiftCommand { get; }
        public ICommand OpenFileDialogForPicture {  get; }

        public AddGiftViewModel(GiftService giftService, GiftViewModel giftViewModel, ContactService contactService, EventService eventService)
        {
            _giftService = giftService;
            _contactService = contactService;
            _eventService = eventService;

            AddGiftCommand = new AddGiftCommand(giftService, this, giftViewModel);
            OpenFileDialogForPicture = new OpenFileDialogForPictureCommand(this);
            LoadContacts();
            LoadEvents();
            LoadStatuses();

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
    }


}
