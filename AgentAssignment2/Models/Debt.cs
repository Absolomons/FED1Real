using System;
namespace DebtBook.Models
{
    public class Debt
    {
        private string? date;
        private double debtValue;
        public Debt()
        {
        } 

        public Debt(double ddebt)
        {
            date = DateTime.Now.ToLongTimeString();
            debtValue = ddebt;
        }

        public double DebtValue 
        { 
            
            get { return debtValue; }
            set { debtValue = value; }
            
        }
        public string? Date
        {

            get { return date; }
            set { date = value; }

        }

    }
}
