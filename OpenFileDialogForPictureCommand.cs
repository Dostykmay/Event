using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using GiftNotation.ViewModels;


namespace GiftNotation.Commands.GiftCommands
{
    public class OpenFileDialogForPictureCommand : ICommand
    {
        private AddGiftViewModel _addGiftViewModel;
        private ChangeGiftViewModel _changeGiftViewModel;

        public OpenFileDialogForPictureCommand(AddGiftViewModel addGiftViewModel)
        {
            _addGiftViewModel = addGiftViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            // Используем OpenFileDialog для выбора изображения
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Выберите изображение"
            };

            if (dialog.ShowDialog() == true)
            {
                _addGiftViewModel.GiftPic = dialog.FileName;
            }
        }
        
    }
    
}
