using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace DebtBook.Models
{
    public class debtor
    {
        string? name;
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
                name = value;
            }
        }

        public double? Debt
        {
            get
            {
                double? totalDebt = 0;
                foreach(Debt d in debtList)
                {
                    totalDebt += d.DebtValue;
                };
                
                return totalDebt;
            }
        }

        List<Debt> debtList = new List<Debt>();

        public List<Debt> DebtList
        {
            get { return debtList; }

            set { debtList = value; }
        }

    }
}
