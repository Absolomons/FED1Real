using DebtBook.ViewModels;
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

namespace DebtBook.Views
{

    public partial class AddView : Window
    {
        public AddView()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AddViewModel;
            if (vm.IsValid)
                DialogResult = true;
            else
                MessageBox.Show("Enter values for Name and Initial debt amount", "Missing data");
        }
    }
}