using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DeptBook.Models
{
    public class Debits : BindableBase
    {
        private float debt;
        private DateTime datetime;

        public Debits(){ }
        
        public Debits(float d, DateTime dt)
        {
            debt = d;
            datetime = dt;
        }

        public float Debt
        {
            get { return debt; }
            set { SetProperty(ref debt, value); }
        }

        public DateTime Datetime
        {
            get { return datetime; }
            set { SetProperty(ref datetime, value); }
        }
        
    }
}
