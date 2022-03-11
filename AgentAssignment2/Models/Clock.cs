using Prism.Mvvm;
using System;

namespace DebtBook.Models
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
