using System;
using System.Windows;

namespace InitiativeRoller.Helpers
{
    /// <summary>
    /// Solution from https://stackoverflow.com/a/27676846/1237309
    /// </summary>
    public static class WindowManager
    {
        public static void CloseWindow(Guid id)
        {
            foreach (Window window in Application.Current.Windows)
            {
                var view = window.DataContext as IRequireViewIdentification;
                if (view?.ViewId.Equals(id) == true)
                    window.Close();
            }
        }
    }
}
