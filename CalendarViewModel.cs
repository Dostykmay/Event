using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftNotation.Models;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Metadata;
using GiftNotation.State.Navigators;
using GiftNotation.Services;

namespace GiftNotation.ViewModels
{
    public class CalendarViewModel : ViewModelBase
    {

        private readonly EventService _eventService;

        private ObservableCollection<DateTime> events = new ObservableCollection<DateTime>();
        public ObservableCollection<DateTime> Events 
        { 
            get => events;
            set => SetProperty(ref events, value);
        }


        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CalendarViewModel(EventService eventService)
        {
            _eventService = eventService;
            LoadEvents();
        }

        private async void LoadEvents()
        {
            var events = await _eventService.GetAllEventDates();
            Events = new ObservableCollection<DateTime>(events);
        }

    }
}
    
