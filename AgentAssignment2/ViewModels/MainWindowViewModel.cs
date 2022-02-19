using AgentAssignment2.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace AgentAssignment2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        ObservableCollection<Agent> agents = new ObservableCollection<Agent>();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindowViewModel()
        {
            agents.Add(new Agent("001", "Nina", "Assassination", "UpperVolta"));
            agents.Add(new Agent("007", "James Bond", "Martinis", "North Korea"));
            CurrentAgent = agents[0];

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        #region Properties

        Agent? currentAgent = null;

        public Agent? CurrentAgent
        {
            get { return currentAgent; }
            set { SetProperty(ref currentAgent, value); }
        }

        public ObservableCollection<Agent> Agents
        {
            get { return agents; }
            set { SetProperty(ref agents, value); }
        }

        private int currentIndex;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set { SetProperty(ref currentIndex, value); }
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
            if (CurrentIndex < (Agents.Count - 1))
                CurrentIndex++;
        }

        bool CanExecuteNextCommand()
        {
            if (CurrentIndex < (Agents.Count - 1))
                return true;
            else
                return false;
        }

        private DelegateCommand? addCommand;
        public DelegateCommand AddCommand =>
            addCommand ?? (addCommand = new DelegateCommand(ExecuteAddCommand));

        void ExecuteAddCommand()
        {
            Agents.Add(new Agent());
            CurrentIndex = Agents.Count - 1;
        }

        private DelegateCommand? deleteCommand;
        public DelegateCommand DeleteCommand =>
            deleteCommand ?? (deleteCommand = new DelegateCommand(ExecuteDeleteCommand, DeleteAgent_CanExecute)
                    .ObservesProperty(() => CurrentIndex));

        void ExecuteDeleteCommand()
        {
            if (CurrentAgent != null)
                Agents.Remove(CurrentAgent);
        }

        private bool DeleteAgent_CanExecute()
        {
            if (Agents.Count > 0 && CurrentIndex >= 0)
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
        #endregion Commands
    }
}
