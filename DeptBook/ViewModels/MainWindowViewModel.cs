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
            debtors = new ObservableCollection<Debtor>();
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
