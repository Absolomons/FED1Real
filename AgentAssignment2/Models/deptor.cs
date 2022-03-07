namespace DebtBook.Models
{
    public class debtor
    {
        string? name;
        double? debt;
        public debtor()
        {
        } 

        public debtor(string dname, double ddebt)
        {
            name = dname;
            debt = ddebt;
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
                return debt;
            }
            set
            {
                if (debt != null) debt += value;
            }
        }
    }
}
