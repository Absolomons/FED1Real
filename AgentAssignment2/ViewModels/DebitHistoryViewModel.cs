using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using DebitHistory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DebitHistory.ViewModels
{
    public class SettingsDialogViewModel : BindableBase, IDialogAware
    {
        #region ApplicationProperties
        private string _title = "Settings dialog";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Report _report;
        public Report Report
        {
            get { return _report; }
            set { SetProperty(ref _report, value); }
        }
        #endregion ApplicationProperties

        #region Commands
        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "<Pending>")]
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.OK;
                // Use the Application object to transfer data to the MainWindow
                ((App)Application.Current).Report = Report;
            }
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1030:Use events where appropriate", Justification = "<Pending>")]
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {

        }

        private DelegateCommand _reportColorCommand;
        public DelegateCommand ReportColorCommand =>
            _reportColorCommand ?? (_reportColorCommand = new DelegateCommand(ReportColorHandler));

        private void ReportColorHandler()
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            Color color = Report.ReportColor;
            dlg.Color = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Report.ReportColor = Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B);
            }
            dlg.Dispose();
        }
        #endregion Commands

        #region events
        public event Action<IDialogResult> RequestClose;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            Report = ((App)Application.Current).Report;
        }

        #endregion events




    }
}
