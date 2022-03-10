using DebtBook.Models;
using DebtBook.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using DebitHistory;
using DebtBook.Data;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace DebtBook.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        ObservableCollection<debtor> deptors = new ObservableCollection<debtor>();
        DispatcherTimer timer = new DispatcherTimer();
        private string filePath = "";

        public MainWindowViewModel()
        {
            
            deptors.Add(new debtor("Jens Jensen", 0));
            //deptors.Add(new debtor("George Freeman", -20));
            //deptors.Add(new debtor("Clint Eastwood", 420));
            //deptors.Add(new debtor("Harboe Cola", 3.95));

            CurrentDeptor = deptors[0];

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        #region Properties

        debtor? currentDeptor = null;

        private string filename = "";
        public string Filename
        {
            get { return filename; }
            set
            {
                SetProperty(ref filename, value);
                RaisePropertyChanged("Title");
            }
        }
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
            set
            {
                double temp = Convert.ToDouble(value);
                SetProperty(ref debtbox, temp);
            }

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
            var debtor = new debtor();
            var vm = new AddViewModel(debtor)
            {
                //Specialities = specialities
            };
            var dlg = new AddView
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };
            if (dlg.ShowDialog() == true)
            {
                Deptors.Add(debtor);
                CurrentDeptor = debtor; // Or CurrentIndex = Agents.Count - 1;
            }
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
            Debtbox = 0.0;
            RaisePropertyChanged("CurrentDeptor");
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
        DelegateCommand _SaveAsCommand;
        public DelegateCommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new DelegateCommand(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Agent assignment documents|*.agn|All Files|*.*",
                DefaultExt = "agn"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                SaveFile();
            }
        }

        private void SaveFile()
        {
            try
            {
                Repository.SaveFile(filePath, deptors);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        DelegateCommand _OpenFileCommand;
        public DelegateCommand OpenFileCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new DelegateCommand(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Agent assignment documents|*.agn|All Files|*.*",
                DefaultExt = "agn"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                try
                {
                    Deptors = Repository.ReadFile(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion Commands
    }
}
