using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAssignment2.Models
{
    public class Clock : BindableBase
    {
        public Clock()
        {
            Update();
        }

        public void Update()
        {
            Date = DateTime.Now.ToLongDateString();
            Time = DateTime.Now.ToLongTimeString();
        }

        string? date;
        public string? Date
        {
            get { return date; }
            private set { SetProperty(ref date, value); }
        }

        string? time;
        public string? Time
        {
            get { return time; }
            private set { SetProperty (ref time, value); }
        }
    }
}
