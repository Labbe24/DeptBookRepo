using Prism.Mvvm;

namespace DeptBook.Models
{
    public class Debtor
    {
        private string _name;
        private float _debt;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public float Debt
        {
            get
            {
                return _debt;
            }
            set
            {
                _debt = value;
            }
        }

        public Debtor() { }
        public Debtor(string name, float debtcredit)
        {
            _name = name;
            _debt = debtcredit;
        }
    }
}