using GiftNotation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftNotation.ViewModels.Factories
{
    public class HomeViewModelFactory : IGiftNotationViewModelFactory<CalendarViewModel>
    {
        private readonly EventService _eventService;

        public HomeViewModelFactory(EventService eventService)
        {
            _eventService = eventService;
        }

        public CalendarViewModel CreateViewModel() { 

            return new CalendarViewModel(_eventService);
        }
    }
}
