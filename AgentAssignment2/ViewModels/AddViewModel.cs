using DebtBook.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DebtBook.ViewModels
{
    public class AddViewModel : BindableBase
    {
        public AddViewModel(debtor debtor)
        {
            CurrentDebtor = debtor;
        }


        debtor currentDebtor;

        public debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set { SetProperty(ref currentDebtor, value); }
        }

        bool isValid;

        public bool IsValid
        {
            get
            {
                isValid = true;
                if (string.IsNullOrWhiteSpace(CurrentDebtor.Name))
                    isValid = false;
                return isValid;
            }
        }

        ICommand _okBtnCommand;

        public ICommand OkBtnCommand
        {
            get
            {
                return _okBtnCommand ??= new DelegateCommand(
                        OkBtnCommand_Execute, OkBtnCommand_CanExecute)
                    .ObservesProperty(() => CurrentDebtor.Name)
                    .ObservesProperty(() => CurrentDebtor.Debt);
            }
        }

        private void OkBtnCommand_Execute()
        {
            // No action here - is handled i code behind
        }

        private bool OkBtnCommand_CanExecute()
        {
            return IsValid;
        }
    }
}