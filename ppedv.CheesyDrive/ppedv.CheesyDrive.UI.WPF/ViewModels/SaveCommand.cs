using ppedv.CheesyDrive.Model.Contracts;
using System;
using System.Windows.Input;

namespace ppedv.CheesyDrive.UI.WPF.ViewModels
{
    public class SaveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        IRepository repo;

        public SaveCommand(IRepository repo)
        {
            this.repo = repo;
        }

        public void Execute(object parameter)
        {
            repo.Save();
        }
    }
}
