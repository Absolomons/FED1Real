﻿using AgentAssignment2.ViewModels;
using System.Windows;

namespace AgentAssignment2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAgents.SelectedIndex > 0)
                lbxAgents.SelectedIndex = --lbxAgents.SelectedIndex;
        }

        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAgents.SelectedIndex < lbxAgents.Items.Count - 1)
                lbxAgents.SelectedIndex = ++lbxAgents.SelectedIndex;
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            vm.AddNewAgent();
            lbxAgents.SelectedIndex = lbxAgents.Items.Count - 1;
            tbxId.Focus();
        }
    }
}
