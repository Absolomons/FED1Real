using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DebitHistory.Models
{
    public class Report : BindableBase
    {
        private Color _reportColor = Colors.YellowGreen;
        public Color ReportColor
        {
            get
            {
                return _reportColor;
            }
            set
            {
                SetProperty(ref _reportColor, value);
            }
        }

        private string _reportFolder = @"C:\Reports";
        public string ReportFolder
        {
            get
            {
                return (_reportFolder);
            }
            set
            {
                SetProperty(ref _reportFolder, value);
            }
        }

        private string _reporter = "";
        public string Reporter
        {
            get
            {
                return (_reporter);
            }
            set
            {
                SetProperty(ref _reporter, value);
            }
        }
    }
}