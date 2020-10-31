using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DeptBook.Models;
using DeptBook.Views;
using Unity.Injection;
using System.Windows;
using DeptBook.Data;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;

namespace DeptBook.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<Debtor> debtors;
        private readonly string AppTitle = "DeptBook";
        private string filePath = "";

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

        #region Properties

        public ObservableCollection<Debtor> Debtors 
        {
            get { return debtors; }
            set { SetProperty(ref debtors, value); }
        }

        Debtor currentDebtor = null;
        public Debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set { SetProperty(ref currentDebtor, value); }
        }

        int currentIndex = -1;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set { SetProperty(ref currentIndex, value); }
        }


        //Top
        private string filename = "";
        public string Filename
        {
            get { return filename; }
            set
            {
                SetProperty(ref filename, value);
                RaisePropertyChanged("Title");
            }
        }

        #endregion

        #region Commands

        ICommand _newCommand;
        public ICommand AddNewDebtorCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new DelegateCommand(() =>
                {
                    var newDebtor = new Debtor();
                    var vm = new DebtorViewModel("Add new debtor", newDebtor);
                    var dlg = new DebtorView
                    {
                        DataContext = vm
                    };
                    if (dlg.ShowDialog() == true)
                    {
                        Debtors.Add(newDebtor);
                        CurrentDebtor = newDebtor;
                        CurrentDebtor.UpdateTotal();
                        
                    }
                }));
            }
        } 
        
        ICommand _editCommand;
        public ICommand EditDebtorCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new DelegateCommand(() =>
                {
                    var tempDebtor = CurrentDebtor.Clone();
                    var vm = new DebitViewModel("Edit debtor", tempDebtor);
                    var dlg = new DebitView
                    {
                        DataContext = vm,
                        Owner = App.Current.MainWindow
                    };
                    if (dlg.ShowDialog() == true)
                    {    
                        CurrentDebtor.TotalDebt = tempDebtor.Sum();
                        CurrentDebtor.Debits = tempDebtor.Debits;
                    }
                }));
            }
        }


        ICommand _NewFileCommand;
        public ICommand NewFileCommand
        {
            get { return _NewFileCommand ?? (_NewFileCommand = new DelegateCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Debtors.Clear();
                Filename = "";
                Dirty = false;
            }
        }

        public string Title
        {
            get
            {
                var s = "";
                if (Dirty)
                    s = "*";
                return Filename + s + " - " + AppTitle;
            }
        }

        private bool dirty = false;
        public bool Dirty
        {
            get { return dirty; }
            set
            {
                SetProperty(ref dirty, value);
                RaisePropertyChanged("Title");
            }
        }

        ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new DelegateCommand<string>(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute(string argFilename)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Dept Book documents|*.agn|All Files|*.*",
                DefaultExt = "DBD"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                try
                {
                    Repository.ReadFile(filePath, out ObservableCollection<Debtor> tempDebtors);
                    Debtors = tempDebtors;
                    Dirty = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        ICommand _SaveAsCommand;
        public ICommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new DelegateCommand<string>(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute(string dbdFilename)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Debt Book documents|*.agn|All Files|*.*",
                DefaultExt = "dbd"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                SaveFile();
            }
        }

        ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new DelegateCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)
                    .ObservesProperty(() => Debtors.Count));
            }
        }

        private void SaveFileCommand_Execute()
        {
            SaveFile();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return (filename != "") && (Debtors.Count > 0);
        }

        private void SaveFile()
        {
            try
            {
                Repository.SaveFile(filePath, Debtors);
                Dirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        ICommand _closeAppCommand;
        public ICommand CloseAppCommand
        {
            get
            {
                return _closeAppCommand ?? (_closeAppCommand = new DelegateCommand(() =>
                {
                    Application.Current.MainWindow.Close();
                }));
            }
        }

        ICommand _closingCommand;
        public ICommand ClosingCommand
        {
            get
            {
                return _closingCommand ?? (_closingCommand = new
                    DelegateCommand<CancelEventArgs>(ClosingCommand_Execute));
            }
        }

        private void ClosingCommand_Execute(CancelEventArgs arg)
        {
            if (Dirty)
                arg.Cancel = UserRegrets();
        }

        private bool UserRegrets()
        {
            var regret = false;
            MessageBoxResult res = MessageBox.Show("You have unsaved data. Are you sure you want to close the application without saving data first?",
                "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.No)
            {
                regret = true;
            }
            return regret;
        }


        #endregion

    }
}
