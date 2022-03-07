using System;
using System.Collections.Generic;
namespace DebtBook.Models

{
    public class debtor
    {
        string? name;
        List<Debt> debt = new List<Debt>();
        public debtor()
        {
        } 

        public debtor(string dname, Debt ddebt)
        {
            name = dname;
            debt.Add(ddebt);
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

        //public double? Debt
        //{
        //    get
        //    {
        //        return debt;
        //    }
        //    set
        //    {
        //        if (debt != null) debt += value;
        //    }
        //}
    }
}
