using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using DeptBook.Models;
using System.Collections.ObjectModel;

namespace DeptBook.ViewModels
{
    public class DebitsViewModel : BindableBase
    {
        public DebitsViewModel()
        {
            debits = new ObservableCollection<Debits>
            {
                #if DEBUG
                new Debits(10, new DateTime(2020, 30, 10)),
                new Debits(120, new DateTime(2020, 30, 10)),
                new Debits(-100, new DateTime(2020, 30, 10)),
                new Debits(122, new DateTime(2020, 30, 10))
                #endif
            };
        }

        private ObservableCollection<Debits> debits;
        public ObservableCollection<Debits> Debits
        {
            get { return debits; }
            set { SetProperty(ref debits, value); }
        }
    }
}
