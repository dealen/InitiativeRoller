using InitiativeRoller.Helpers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitiativeRoller.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ViewModelCommand _commandCloseWindow;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertiesChanged(params string[] properties)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(properties[i]));
            }
        }


        #region Public Commands

        public ViewModelCommand CommandCloseWindow
        {
            get { return _commandCloseWindow ?? (_commandCloseWindow = new ViewModelCommand(_CloseWindow)); }
        }

        #endregion



        #region Private Methods

        private void _CloseWindow(object obj)
        {
            if (obj == null) throw new ArgumentNullException("Brak viewId");
            Guid viewId = (Guid)obj;
            WindowManager.CloseWindow(viewId);
        }

        #endregion
    }
}
