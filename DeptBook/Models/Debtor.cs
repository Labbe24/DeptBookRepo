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
        

        private string _name;
        private float _totaldebt;
        private float _debt;

        private ObservableCollection<Debit> debits;
        public ObservableCollection<Debit> Debits
        {
            get { return debits; }
            set 
            { 
                SetProperty(ref debits, value);
                UpdateTotal();
            }
        }

        public Debtor Clone()
        {
            return this.MemberwiseClone() as Debtor;
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public float TotalDebt
        {
            get { return _totaldebt; }
            set { SetProperty(ref _totaldebt, value); }
        }

        public float Debt
        {
            get { return _debt; }
            set
            {
                Debits.Add(new Models.Debit(value, DateTime.Now));
                UpdateTotal();
            }
        }

        public void UpdateTotal()
        {
            TotalDebt = Sum();
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
            Debits = new ObservableCollection<Debit>();
        }

        public Debtor(string name, float debtcredit)
        {
            _name = name;
            Debits = new ObservableCollection<Debit>();
            Debits.Add(new Models.Debit(debtcredit, DateTime.Now));
            UpdateTotal();
        }

        
    }
}