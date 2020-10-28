using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DeptBook.Models;

namespace DeptBook.ViewModels
{
    public class DebtorViewModel
    {
        public DebtorViewModel(string title, Debtor debtor)
        {
            Title = title;
            CurrentDebtor = debtor;
        }

        string title;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
            }
        }

        private Debtor currentDebtor;
        public Debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set { currentDebtor = value; }
        }

        public bool IsValid
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(CurrentDebtor.Name))
                    isValid = false;
                return isValid;
            }
            //set
            //{
            //    SetProperty(ref isValid, value);
            //}
        }

        ICommand _addBtnCommand;
        public ICommand AddBtnCommand
        {
            get
            {
                return _addBtnCommand ?? (_addBtnCommand = new DelegateCommand(
                    AddBtnCommand_Execute, AddBtnCommand_CanExecute)
                    .ObservesProperty(() => CurrentDebtor.Name));
            }
        }

        private void AddBtnCommand_Execute()
        {

        }

        private bool AddBtnCommand_CanExecute()
        {
            return IsValid;
        }
    }
}
