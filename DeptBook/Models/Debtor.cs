﻿using Prism.Mvvm;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeptBook.Models
{
    public class Debtor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName]string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string _name;
        private float _debt;

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