﻿using DebtBook.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DebtBook.ViewModels
{
    public class DebtBookViewModel : BindableBase
    {
        public DebtBookViewModel(debtor debtor)
        {
            Title = debtor.Name;
            CurrentDebtor = debtor;
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
        debtor currentDebtor;

        public debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set
            {
                SetProperty(ref currentDebtor, value);
            }
        }

        #endregion

    }
}
