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

        #region Properties

        string title;

        public string Title
        {
            get { return title; }
            set
            {
                SetProperty(ref title, value);
            }
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

        private bool isValid = true;
        public bool IsValid
        {
            get
            {
                // bool isValid = true;
                if (string.IsNullOrWhiteSpace(CurrentDebtor.Name))
                {
                    isValid = false;
                }if (string.IsNullOrWhiteSpace(CurrentDebtor.Debt.ToString()))
                {
                    isValid = false;
                }
                 
                return isValid;
            }
            set
            {
                SetProperty(ref isValid, value);
            }
        }

        #endregion

        #region Commands

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
            return true; ;
        }

        #endregion
    }
}
