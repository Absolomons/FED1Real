using System;
namespace DebtBook.Models
{
    public class Debt
    {
        string? date;
        double? debtValue;
        public Debt()
        {
        } 

        public Debt(double ddebt)
        {
            date = DateTime.Now.ToLongTimeString();
            debtValue = ddebt;
        }

        public double? DebtValue 
        { 
            
            get { return debtValue; }
            
        }
       
    }
}
