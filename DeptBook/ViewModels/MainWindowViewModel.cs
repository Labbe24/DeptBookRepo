using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DeptBook.Models;
using Prism.Mvvm;

namespace DeptBook.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<Debtor> debtors;

        public MainWindowViewModel()
        {
            debtors = new ObservableCollection<Debtor>
            {
                #if DEBUG
                new Debtor("Thomas Gammelby", 100),
                new Debtor("Jens Nørby Kristensen", 1200),
                new Debtor("Joachim Leth Krøyer", -1000),
                new Debtor("Andreas Støve", 12200)
                #endif
            };
        }

        public ObservableCollection<Debtor> Debtors 
        {
            get
            {
                return debtors;
            }
            set
            {
                debtors = value;
            }
        }
    }
}
