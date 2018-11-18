using InitiativeRoller.Helpers;
using InitiativeRoller.Models;
using InitiativeRoller.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeRoller.ViewModels
{
    public class ViewModelMainWindow : ViewModel, IRequireViewIdentification
    {
        #region Private Fields

        private string _newCharacterName;
        private int _initiativeModyfier;
        private ViewModelCommand _commandAddCharacter;
        private ObservableCollection<Character> _characters;
        private bool _isDnD;
        private bool _isWH;
        private bool _isCoC;
        private ViewModelCommand _commandBeginBattle;
        private Guid _viewId;

        #endregion



        #region Public Constructor

        public ViewModelMainWindow()
        {
            Initialize();
        }

        #endregion




        #region Public Properties

        public string NewCharacterName
        {
            get { return _newCharacterName; }
            set
            {
                _newCharacterName = value;
                OnPropertyChanged();
            }
        }

        public int InitiativeModyfier
        {
            get { return _initiativeModyfier; }
            set
            {
                _initiativeModyfier = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Character> Characters
        {
            get { return _characters; }
            set
            {
                _characters = value;
                OnPropertyChanged();
            }
        }

        public bool IsDungeonsAndDragons
        {
            get { return _isDnD; }
            set
            {
                _isDnD = value;
                OnPropertyChanged();
            }
        }

        public bool IsWarhammer
        {
            get { return _isWH; }
            set
            {
                _isWH = value;
                OnPropertyChanged();
            }
        }

        public bool IsCallOfCthulhu
        {
            get { return _isCoC; }
            set
            {
                _isCoC = value;
                OnPropertyChanged();
            }
        }

        #endregion



        #region Public Commands

        public ViewModelCommand CommandAddCharacter
        {
            get { return _commandAddCharacter ?? (_commandAddCharacter = new ViewModelCommand(_AddCharacter)); }
        }

        public ViewModelCommand CommandBeginBattle
        {
            get { return _commandBeginBattle ?? (_commandBeginBattle = new ViewModelCommand(_BeginBattle)); }
        }

        public Guid ViewId => _viewId;

        #endregion



        #region Private Methods

        private void _AddCharacter(object parameters)
        {
            if (parameters == null) return;

            var p = parameters as object[];

            var characterName = p[0].ToString();
            int.TryParse(p[1].ToString(), out int initiativeModifier); // iniline declaration

            if (Characters.Any(x => x.InitiativeModificator == initiativeModifier && x.Name == characterName)) return;

            Random random = new Random();
            Characters.Add(new Character(characterName, initiativeModifier, random.Next(1, GetInitiativeRollDiceNumber())));
            Characters = new ObservableCollection<Character>(Characters
                .OrderByDescending(x => x.InitiativeRoll)
                .ThenByDescending(x => x.InitiativeModificator).ToList());
        }

        private int GetInitiativeRollDiceNumber()
        {
            if (IsWarhammer)
                return 10;
            if (IsDungeonsAndDragons)
                return 20;
            if (IsCallOfCthulhu)
                return 10;
            throw new ArgumentNullException("System nie jest wybrany i nie da się zakończyć operacji.");
        }

        private void _BeginBattle(object obj)
        {
            var dialog = new BattleWindow();
            dialog.Tag = Characters;

            dialog.Closing += delegate
            {

            };

            dialog.ShowDialog();
        }

        #endregion



        #region Public Methods

        public void Initialize()
        {
            _viewId = Guid.NewGuid();
            Characters = new ObservableCollection<Character>();
            IsWarhammer = true;

#if DEBUG
            Random random = new Random();
            Characters.Add(new Character("Jan", random.Next(20, 50), random.Next(1, GetInitiativeRollDiceNumber())));
            Characters.Add(new Character("Stefan", random.Next(20, 50), random.Next(1, GetInitiativeRollDiceNumber())));
            Characters.Add(new Character("Banita", random.Next(20, 50), random.Next(1, GetInitiativeRollDiceNumber())));
            Characters.Add(new Character("Krystyna", random.Next(20, 50), random.Next(1, GetInitiativeRollDiceNumber())));
            Characters.Add(new Character("Anna", random.Next(20, 50), random.Next(1, GetInitiativeRollDiceNumber())));

            Characters = new ObservableCollection<Character>(Characters
                .OrderByDescending(x => x.InitiativeRoll)
                .ThenByDescending(x => x.InitiativeModificator).ToList());
#endif
        }

        #endregion
    }
}
