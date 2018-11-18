using InitiativeRoller.ViewModels;
using System.Collections;
using System.Windows;

namespace InitiativeRoller.Views
{
    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        private ViewModelBattleWindow _vm;

        public BattleWindow()
        {
            InitializeComponent();
        }

        private ViewModelBattleWindow VM
        {
            get
            {
                if (_vm == null)
                {
                    _vm = (ViewModelBattleWindow)DataContext;
                }

                return _vm;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = VM;
            VM.Initialize(this.Tag as IEnumerable);
        }
    }
}
