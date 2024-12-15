using GiftNotation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftNotation.ViewModels
{
    public class ChangeContactViewModel : ViewModelBase
    {
        private readonly ContactService _contactService;
        private readonly GiftService _giftService;

        public ChangeContactViewModel(ContactService contactService, GiftService giftService, ContactViewModel contactViewModel)
        {
            _contactService = contactService;
            _giftService = giftService;
        }
    }
}
