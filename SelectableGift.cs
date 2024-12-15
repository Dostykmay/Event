using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GiftNotation.Models;

namespace GiftNotation.ViewModels
{
    public class SelectableGift : ViewModelBase
    {
        public Gifts Gift { get; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public SelectableGift(Gifts gift)
        {
            Gift = gift;
        }
    }
}
