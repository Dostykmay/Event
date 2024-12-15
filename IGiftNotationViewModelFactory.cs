using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftNotation.ViewModels.Factories
{
    public interface IGiftNotationViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}
