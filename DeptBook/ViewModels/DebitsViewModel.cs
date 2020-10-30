using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using DeptBook.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DeptBook.Views;
using Prism.Commands;

namespace DeptBook.ViewModels
{
    public class DebitsViewModel : BindableBase
    {
        public DebitsViewModel(string title, Debtor debtor)
        {
            CurrentDebtor = debtor;
            Title = title;
            //debits = new ObservableCollection<Debits>
            //{
            //    #if DEBUG
            //    new Debits(10, new DateTime(2020, 30, 10)),
            //    new Debits(120, new DateTime(2020, 30, 10)),
            //    new Debits(-100, new DateTime(2020, 30, 10)),
            //    new Debits(122, new DateTime(2020, 30, 10))
            //    #endif
            //};
        }

        string title;

        public string Title
        {
            get { return title; }
            set
            {
                SetProperty(ref title, value);
            }
        }

        private float debtvalue;

        public float DebtValue
        {
            get { return debtvalue;}
            set { SetProperty(ref debtvalue, value); }
        }

        private ObservableCollection<Debits> debits;
        public ObservableCollection<Debits> Debits
        {
            get { return debits; }
            set { SetProperty(ref debits, value); }
        }

        private Debtor currentDebtor;
        public Debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set
            {
                SetProperty(ref currentDebtor, value);
            }
        }

        ICommand _addDebitCommand;
        public ICommand AddDebitCommand
        {
            get
            {
                return _addDebitCommand ?? (_addDebitCommand = new DelegateCommand(() =>
                {

                    CurrentDebtor.Debits.Add(new Models.Debits(DebtValue, DateTime.Now));
  
                }));
            }
        }

        ICommand _saveBtnCommand;
        public ICommand SaveBtnCommand
        {
            get
            {
                return _saveBtnCommand ?? (_saveBtnCommand = new DelegateCommand(
                        SaveBtnCommand_Execute, SaveBtnCommand_CanExecute)
                    .ObservesProperty(() => CurrentDebtor.Name)
                    .ObservesProperty(() => CurrentDebtor.Debt));
            }
        }

        private void SaveBtnCommand_Execute()
        {
            // Nothing needs to be done here
        }

        private bool SaveBtnCommand_CanExecute()
        {
            // return IsValid;
            return true;
        }
    }
}
