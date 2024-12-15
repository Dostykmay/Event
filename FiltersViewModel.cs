using GiftNotation.Commands.GiftCommands;
using GiftNotation.Models;
using GiftNotation.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftNotation.ViewModels
{
    public class FiltersViewModel : ViewModelBase
    {
        private readonly ContactService _contactService;
        private readonly EventService _eventService;

        private string? _selectedMonth;
        private EventType? _selectedEventType;
        private RelpType? _selectedRelpType;

        public ObservableCollection<EventType> EventTypes { get; private set; } = new ObservableCollection<EventType>();
        public ObservableCollection<RelpType> RelpTypes { get; private set; } = new ObservableCollection<RelpType>();
        public ObservableCollection<string> Month { get; private set; } = new ObservableCollection<string>() {"Январь", "Февраль", "Март", "Апрель", "Май", 
            "Июнь", "Июль", "Август", "Сунтябрь", "Октябрь", "Ноябрь", "Декабрь" };

        public string? SelectedMonth
        {
            get => _selectedMonth;
            set => SetProperty(ref _selectedMonth, value);

        }

        public EventType? SelectedEventType
        {
            get => _selectedEventType;
            set => SetProperty(ref _selectedEventType, value);
        }

        public RelpType? SelectedRelpType
        {
            get => _selectedRelpType;
            set => SetProperty(ref _selectedRelpType, value);
        }

        public FiltersViewModel(ContactService contactService, EventService eventService)
        {
           _contactService = contactService;
            _eventService = eventService;
            LoadRelpTypes();
            LoadEventTypes();
        }

        public async void LoadRelpTypes()
        {
            var relpTypes = await _contactService.GetAllRelpTypes();
            foreach (var relpType in relpTypes)
            {
                RelpTypes.Add(relpType);
            }
        }

        public async void LoadEventTypes()
        {
            var eventTypes = await _eventService.GetEventTypesAsync();
            foreach (var eventType in eventTypes)
            {
                EventTypes.Add(eventType);
            }
        }

    }
}
