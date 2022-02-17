using AgentAssignment2.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgentAssignment2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        ObservableCollection<Agent> agents = new ObservableCollection<Agent>();

        public MainWindowViewModel()
        {
            agents.Add(new Agent("001", "Nina", "Assassination", "UpperVolta"));
            agents.Add(new Agent("007", "James Bond", "Martinis", "North Korea"));
            CurrentAgent = agents[0];
        }

        #region Properties

        Agent? currentAgent = null;

        public Agent CurrentAgent
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

        #endregion

        #region Methods

        public void AddNewAgent()
        {
            agents.Add(new Agent());
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

        #endregion Commands
    }
}
