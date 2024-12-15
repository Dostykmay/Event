using GiftNotation.Commands;
using GiftNotation.Commands.EventCommands;
using GiftNotation.Models;
using GiftNotation.Services;
using GiftNotation.Views;
using GiftNotation.State.Navigators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GiftNotation.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class EventViewModel : ViewModelBase
    {
        private ObservableCollection<DisplayEventModel> _events;
        private readonly EventService _eventService;
        private readonly ContactService _contactService;
        private readonly FiltersViewModel _filtersViewModel;

        public DisplayEventModel selectedEvent;
        private Window? _filtersWindow;

        private bool _isFiltersWindowVisible;

        public bool IsFiltersWindowVisible
        {
            get => _isFiltersWindowVisible;
            set => SetProperty(ref _isFiltersWindowVisible, value);
        }

        public DisplayEventModel SelectedEvent
        {
            get => selectedEvent;
            set
            {
                SetProperty(ref selectedEvent, value);
                ((DeleteEventCommand)DeleteEventCommand).RaiseCanExecuteChanged();
                ((OpenChangeEventCommand)OpenChangeEventCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<DisplayEventModel> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public ICommand DeleteEventCommand { get; set; }
        public ICommand OpenAddEventCommand { get; set; }
        public ICommand OpenChangeEventCommand { get; set; }
        public ICommand OpenCloseFilterCommand { get; set; }

        public EventViewModel(EventService eventService, FiltersViewModel filtersViewModel, ContactService contactService)
        {
            _eventService = eventService;
            _contactService = contactService;
            _filtersViewModel = filtersViewModel;

            DeleteEventCommand = new DeleteEventCommand(this, _eventService);
            OpenAddEventCommand = new OpenAddEventCommand(eventService, this, _contactService);
            OpenChangeEventCommand = new OpenChangeEventCommand(this, _eventService, _contactService);
            OpenCloseFilterCommand = new OpenCloseFilterCommand(this);

            LoadEvents();
        }

        public event EventHandler? ViewModelChanging;

        public void ToggleFiltersWindow()
        {
            if (_filtersWindow == null || !_filtersWindow.IsVisible)
            {
                // Создаем и отображаем окно
                _filtersWindow = new Filters
                {
                    DataContext = _filtersViewModel,
                    Owner = Application.Current.MainWindow,
                    Topmost = true // Окно всегда поверх других
                };
                _filtersWindow.Closed += (s, e) => _filtersWindow = null; // Сбрасываем переменную при закрытии окна
                _filtersWindow.Show();
            }
            else
            {
                // Закрываем окно
                _filtersWindow.Close();
            }
        }

        public void OnViewModelChanging()
        {
            ViewModelChanging?.Invoke(this, EventArgs.Empty);
        }

        public async void LoadEvents()
        {
            var events = await _eventService.GetEventsAsync();
            Events = new ObservableCollection<DisplayEventModel>(events);
        }
    }

}
