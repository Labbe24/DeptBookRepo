using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DeptBook.Models;
using System.Windows;

namespace DeptBook.ViewModels
{
    public class DebtorViewModel : BindableBase
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
                {
                    isValid = false;
                    MessageBox.Show("No name entered");
                }
                 
                return isValid;
            }
            //set
            //{
            //    SetProperty(ref isValid, value);
            //}
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
            return IsValid;
        }


    }
}
