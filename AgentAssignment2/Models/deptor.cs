using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Prism.Mvvm;

namespace DebtBook.Models
{
    public class debtor : BindableBase
    {
        string? name;
        double debt;
        public debtor()
        {
        }

        public debtor Clone()
        {
            return this.MemberwiseClone() as debtor;
        }

        public debtor(string dname, double ddebt)
        {
            name = dname;
            Debt temp = new Debt(ddebt);
            debtList.Add(temp);
        }

        public void addDebt(double ddebt)
        {
            Debt temp = new Debt(ddebt);
            debtList.Add(temp);
        }
        public string? Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);

            }
        }

        public double Debt
        {
            get
            {
                double totalDebt = 0;
                foreach(Debt d in debtList)
                {
                    totalDebt += d.DebtValue;
                };
                
                return totalDebt;
            }
            set {
                addDebt(value);
            }
        }

        List<Debt> debtList = new List<Debt>();

        public List<Debt> DebtList
        {
            get { return debtList; }

            set { debtList = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

}
