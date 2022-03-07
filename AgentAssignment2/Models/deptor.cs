using System;
using System.Collections.Generic;
namespace DebtBook.Models

{
    public class debtor
    {
        string? name;
        List<Debt> debtList = new List<Debt>();
        double debt;
        public debtor()
        {
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
    }
}
