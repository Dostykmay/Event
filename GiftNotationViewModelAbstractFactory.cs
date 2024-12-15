using GiftNotation.Services;
using GiftNotation.State.Navigators;
using GiftNotation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GiftNotation.ViewModels.Factories
{
    public class GiftNotationViewModelAbstractFactory : IGiftNotationViewModelAbstractFactory
    {
        private readonly IGiftNotationViewModelFactory<CalendarViewModel> _calendarViewModelFactory;
        private readonly ContactViewModel _contactViewModel;
        private readonly GiftViewModel _giftViewModel;
        private readonly EventViewModel _eventViewModel;
        private readonly GiftService _giftService;
        private readonly ContactService _contactService;
        private readonly EventService _eventService;
        private readonly FiltersViewModel _filtersViewModel;

        public GiftNotationViewModelAbstractFactory(IGiftNotationViewModelFactory<CalendarViewModel> calendarViewModelFactory,
            ContactViewModel contactViewModel, GiftViewModel giftViewModel, EventViewModel eventViewModel,
            GiftService giftService, ContactService contactService, EventService eventService, FiltersViewModel filtersViewModel)
        {
            _calendarViewModelFactory = calendarViewModelFactory;
            _contactViewModel = contactViewModel;
            _giftViewModel = giftViewModel;
            _eventViewModel = eventViewModel;
            _giftService = giftService;
            _contactService = contactService;
            _eventService = eventService;
            _filtersViewModel = filtersViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Calendar:
                    // Создание новой модели представления для Calendar
                    return _calendarViewModelFactory.CreateViewModel();
                case ViewType.Contacts:
                    return new ContactViewModel(_contactService, _giftService);
                case ViewType.Events:
                    return new EventViewModel(_eventService, _filtersViewModel, _contactService);
                case ViewType.Gifts:
                    return new GiftViewModel(_giftService, _contactService, _eventService);
                default:
                    throw new ArgumentException("Takogo net");
            }
        }
    }
}
