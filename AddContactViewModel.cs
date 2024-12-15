using GiftNotation.Services;
using GiftNotation.Models;
using GiftNotation.Commands.ContactCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GiftNotation.ViewModels
{
    public class AddContactViewModel : ViewModelBase
    {
        private readonly ContactService _contactService;
        private readonly GiftService _giftService;

        private string _contactName;
        private DateTime _bday;
        private RelpType _selectedRelpType;
        private Gifts _selectedGift;
        private Gifts _selectedGiftForContact;
        private AddGiftForContactOnAddCommand _addGiftForContactOnAddCommand;

        public ObservableCollection<Gifts> Gifts { get; set; } = new ObservableCollection<Gifts>();
        public ObservableCollection<Gifts> GiftsForContact { get; set; } = new ObservableCollection<Gifts>();
        public ObservableCollection<RelpType> RelpTypes { get; private set; } = new ObservableCollection<RelpType>();

        // Для хранения выбранных подарков
        public ObservableCollection<Gifts> _selectedGifts = new ObservableCollection<Gifts>();

        public string ContactName
        {
            get => _contactName;
            set => SetProperty(ref _contactName, value);
        }

        public DateTime Bday
        {
            get => _bday;
            set => SetProperty(ref _bday, value);
        }

        public RelpType? SelectedRelpType
        {
            get => _selectedRelpType;
            set => SetProperty(ref _selectedRelpType, value);
        }

        public Gifts SelectedGift
        {
            get => _selectedGift;
            set
            {
                if (SetProperty(ref _selectedGift, value))
                {
                    AddGiftForContactOnAddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public Gifts SelectedGiftForContact
        {
            get => _selectedGiftForContact;
            set
            {
                if (SetProperty(ref _selectedGiftForContact, value))
                {
                    // Уведомляем команду, что условие для выполнения изменилось
                    DeleteGiftForContactCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public AddContactCommand AddContactCommand { get; }
        public AddGiftForContactOnAddCommand AddGiftForContactOnAddCommand => _addGiftForContactOnAddCommand;

        public DeleteGiftForContactOnAddCommand DeleteGiftForContactCommand { get; }

        public AddContactViewModel(ContactService contactService, GiftService giftService, ContactViewModel contactViewModel)
        {
            _contactService = contactService;
            _giftService = giftService;
            LoadGifts();
            LoadRelpTypes();
            AddContactCommand = new AddContactCommand(contactService, contactViewModel, this);
            _addGiftForContactOnAddCommand = new AddGiftForContactOnAddCommand(this);
            DeleteGiftForContactCommand = new DeleteGiftForContactOnAddCommand(this);
        }

        public async void LoadGifts()
        {
            var gifts = await _giftService.GetAllGifts();
            Gifts = new ObservableCollection<Gifts>(gifts);
        }

        public async void LoadRelpTypes()
        {
            var relpTypes = await _contactService.GetAllRelpTypes();
            foreach (var relpType in relpTypes)
            {
                RelpTypes.Add(relpType);
            }
        }   
    }

}
