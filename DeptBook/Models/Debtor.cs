using Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;

namespace DeptBook.Models
{
    public class Debtor : BindableBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName]string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string _name;
        private float _totaldebt;
        private float _debt;

        private ObservableCollection<Debits> debits;
        public ObservableCollection<Debits> Debits
        {
            get { return debits; }
            set { SetProperty(ref debits, value); }
        }

        public Debtor Clone()
        {
            return this.MemberwiseClone() as Debtor;
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public float TotalDebt
        {
            get
            {
                return _totaldebt;
            }
            set
            {
                //SetProperty(ref _totaldebt, value);
                //Debits.Add(new Models.Debits(value, DateTime.Now));
                //Debits.Add(new Models.Debits(_debt, DateTime.Now));
            }
        }

        public void UpdateTotal()
        {
            TotalDebt = Sum();
        }

        
        public float Debt
        {
            get
            {
                return _debt;
            }
            set
            {
                Debits.Add(new Models.Debits(value, DateTime.Now));
                UpdateTotal();
            }
        }

        public float Sum()
        {
            float sum = 0;
            foreach(var debt in Debits)
            {
                sum += debt.Debt;
            }
            return sum;
        }

        public Debtor() 
        {
            Debits = new ObservableCollection<Debits>();
        }
        public Debtor(string name, float debtcredit)
        {
            _name = name;
            Debits = new ObservableCollection<Debits>();
            Debits.Add(new Models.Debits(debtcredit, DateTime.Now));
            _totaldebt = Sum();
        }

        
    }
}