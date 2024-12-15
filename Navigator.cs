using GiftNotation.Commands;
using GiftNotation.Services;
using GiftNotation.ViewModels;
using GiftNotation.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiftNotation.State.Navigators
{


    public class Navigator : ViewModelBase, INavigator
    {
        //Создание экзкмпляра класса ViewModelBase
        private ViewModelBase _currentViewModel;
        private readonly IGiftNotationViewModelAbstractFactory _viewModelfactory;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                //Получаем текущую модель представления
                return _currentViewModel;
            }
            set {
                //Устанавливаем текущую модель представления и сообщаем об изменении
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
                if (_currentViewModel is EventViewModel eventViewModel)
                {
                    // Уведомляем о смене модели представления
                    eventViewModel.OnViewModelChanging();
                }
            }
        }

        public event Action? CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        //Команда выполняющаяся при нажатии на кнопку
        public ICommand UpdateCurrentVMCommand { get; set; }

        public Navigator(IGiftNotationViewModelAbstractFactory viewModelFactory) {

            UpdateCurrentVMCommand = new UpdateCurrentVMCommand(this, viewModelFactory);

        }
    }
}
