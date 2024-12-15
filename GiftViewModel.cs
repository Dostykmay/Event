using GiftNotation.Commands.GiftCommands;
using GiftNotation.Data;
using GiftNotation.Models;
using GiftNotation.Services;
using GiftNotation.State;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;


namespace GiftNotation.ViewModels
{
    public class GiftViewModel : ViewModelBase
    {
        private ObservableCollection<DisplayGiftModel> _gifts;
        private GiftService _giftService;
        private EventService _eventService;
        private ContactService _contactService;

        public DisplayGiftModel selectedGift;

        public DisplayGiftModel SelectedGift
        {
            get => selectedGift;
            set
            {
                SetProperty(ref selectedGift, value);
                ((DeleteGiftCommand)DeleteGiftCommand).RaiseCanExecuteChanged();
                ((OpenChangeGiftCommand)OpenChangeGiftCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<DisplayGiftModel> Gifts
        {
            get { return _gifts; }
            set { SetProperty(ref _gifts, value); }
        }

        public ICommand OpenAddGiftCommand { get; set; }
        public ICommand DeleteGiftCommand { get; set; }
        public ICommand OpenChangeGiftCommand { get; set; }


        public GiftViewModel(GiftService giftService, ContactService contactService, EventService eventService)
        {
            _giftService = giftService;
            _contactService = contactService;
            _eventService = eventService;

            // Загрузка данных из базы данных или другого источника
            LoadGifts();
            OpenAddGiftCommand = new OpenAddGiftCommand(this, _giftService, _eventService, contactService);
            DeleteGiftCommand = new DeleteGiftCommand(this, _giftService);
            OpenChangeGiftCommand = new OpenChangeGiftCommand(this, _giftService, _contactService, _eventService);
        }

        public async void LoadGifts()
        {
            // Здесь вы можете подключиться к базе данных через сервис или репозиторий
            var gifts = await _giftService.GetGiftAsync(); // Это пример, ваш метод получения данных
            Gifts = new ObservableCollection<DisplayGiftModel>(gifts);
        }

    }
}