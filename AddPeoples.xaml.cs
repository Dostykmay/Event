using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GiftNotation.Views
{
    /// <summary>
    /// Логика взаимодействия для AddGifts.xaml
    /// </summary>
    public partial class AddPeoples : Window
    {
        public AddPeoples()
        {
            InitializeComponent();
        }

        private void ButtonAdd_ClosePeople(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
