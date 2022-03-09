using DebtBook.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DebtBook.ViewModels
{
    public class AddViewModel : BindableBase
    {
        private ObservableCollection<debtor> debtorCopy;
        private string name;
        private double debt;
        public AddViewModel(ObservableCollection<debtor> deptors)
        {
            debtorCopy = deptors;
            Title = "Add new debtor";
        }

        #region Properties
        string title;

        public string Title
        {
            get { return title; }
            set
            {
                SetProperty(ref title, value);
            }
        }
        public string Name { get; set; }
        public double Debt { get { return debt; }}
    }



        #endregion

    
}