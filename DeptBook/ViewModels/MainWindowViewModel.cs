using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DeptBook.Models;
using DeptBook.Views;

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
            get { return debtors; }
            set { debtors = value; }
        }

        Debtor currentDebtor = null;
        public Debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set {  currentDebtor = value; }
        }

        int currentIndex = -1;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; }
        }

        ICommand _newCommand;
        public ICommand AddNewDebtorCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new DelegateCommand(() =>
                {
                    var newDebtor = new Debtor();
                    var vm = new DebtorViewModel("Add new agent", newDebtor)
                    {
                        // Specialities = specialities
                    };
                    var dlg = new DebtorView
                    {
                        DataContext = vm
                    };
                    if (dlg.ShowDialog() == true)
                    {
                        Debtors.Add(newDebtor);
                        CurrentDebtor = newDebtor;
                        // CurrentSpecialityIndex = 0;
                        // Dirty = true;
                    }
                }));
            }
        }
    }
}
