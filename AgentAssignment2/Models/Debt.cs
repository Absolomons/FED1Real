namespace DebtBook.Models
{
    public class Debt
    {
        string? name;
        string? date;
        double? debtValue;
        public Debt()
        {
        } 

        public Debt(string dname, string ddate, double ddebt)
        {
            name = dname;
            debtValue = ddebt;
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

        public double? DebtValue
        {
            get
            {
                return debtValue;
            }
            set
            {
                if (debtValue != null) debtValue += value;
            }
        }
    }
}
