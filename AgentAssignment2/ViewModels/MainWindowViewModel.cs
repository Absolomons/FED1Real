using DebtBook.Models;
using DebtBook.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace DebtBook.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        ObservableCollection<debtor> deptors = new ObservableCollection<debtor>();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindowViewModel()
        {
            
            deptors.Add(new debtor("Jens Jensen", 10));
            deptors.Add(new debtor("George Freeman", -20));
            deptors.Add(new debtor("Clint Eastwood", 420));
            deptors.Add(new debtor("Harboe Cola", 3.95));

            CurrentDeptor = deptors[0];

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        #region Properties

        debtor? currentDeptor = null;

        public debtor? CurrentDeptor
        {
            get { return currentDeptor; }
            set { SetProperty(ref currentDeptor, value); }
        }

        public ObservableCollection<debtor> Deptors
        {
            get { return deptors; }
            set { SetProperty(ref deptors, value); }
        }

        private int currentIndex;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set { SetProperty(ref currentIndex, value); }
        }

        private double debtbox;

        public double Debtbox
        {
            get { return this.debtbox; }
            set { debtbox = Convert.ToDouble(value); }

        }




        // No need to notify as it will never change
        Clock clock = new Clock();
        public Clock Clock { get => clock; set => clock = value; }
        #endregion

        #region Methods

        void Timer_Tick(object? sender, EventArgs e)
        {
            clock.Update();
        }

        #endregion
        #region Commands

        private DelegateCommand? _previusCommand;
        public DelegateCommand PreviusCommand =>
            _previusCommand ?? (_previusCommand = new DelegateCommand(ExecutePreviusCommand, CanExecutePreviusCommand)
            .ObservesProperty(() => CurrentIndex));

        void ExecutePreviusCommand()
        {
            if (CurrentIndex > 0)
                --CurrentIndex;
        }

        bool CanExecutePreviusCommand()
        {
            if (CurrentIndex > 0)
                return true;
            else
                return false;
        }

        private DelegateCommand? _nextCommand;
        public DelegateCommand NextCommand =>
            _nextCommand ?? (_nextCommand = new DelegateCommand(ExecuteNextCommand, CanExecuteNextCommand)
            .ObservesProperty(() => CurrentIndex));

        void ExecuteNextCommand()
        {
            if (CurrentIndex < (Deptors.Count - 1))
                CurrentIndex++;
        }

        bool CanExecuteNextCommand()
        {
            if (CurrentIndex < (Deptors.Count - 1))
                return true;
            else
                return false;
        }

        private DelegateCommand? addCommand;
        public DelegateCommand AddCommand =>
            addCommand ?? (addCommand = new DelegateCommand(ExecuteAddCommand));

        void ExecuteAddCommand()
        {
            
        }

        private DelegateCommand? deleteCommand;
        public DelegateCommand DeleteCommand =>
            deleteCommand ?? (deleteCommand = new DelegateCommand(ExecuteDeleteCommand, DeleteDeptor_CanExecute)
                    .ObservesProperty(() => CurrentIndex));

        void ExecuteDeleteCommand()
        {
            if (CurrentDeptor != null)
                Deptors.Remove(CurrentDeptor);
        }

        private bool DeleteDeptor_CanExecute()
        {
            if (Deptors.Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
        }

        private DelegateCommand? closeAppCommand;
        public DelegateCommand CloseAppCommand =>
            closeAppCommand ?? (closeAppCommand = new DelegateCommand(ExecuteCloseAppCommand));

        void ExecuteCloseAppCommand()
        {
            Application.Current.MainWindow.Close();
        }


        private DelegateCommand clickCommand;
        public DelegateCommand ClickCommand => clickCommand ?? (clickCommand = new DelegateCommand(ExecuteClickCommand));
        void ExecuteClickCommand()
        {
            currentDeptor.addDebt(debtbox);
        }


        private DelegateCommand historyCommand;
        public DelegateCommand HistoryCommand => historyCommand ?? (historyCommand = new DelegateCommand(ExecuteHistoryCommand));
        void ExecuteHistoryCommand()
        {

            var tempDebtor = CurrentDeptor.Clone();
            var vm = new DebtBookViewModel(tempDebtor)
            {
                //Specialities = specialities
            };
            var dlg = new DebitHistoryView
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };
            if (dlg.ShowDialog() == true)
            {

            }
        }
        #endregion Commands
    }
}
