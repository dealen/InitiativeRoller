using InitiativeRoller.Helpers;
using InitiativeRoller.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace InitiativeRoller.ViewModels
{
    public class ViewModelBattleWindow : ViewModel, IRequireViewIdentification
    {
        private List<Character> _characters;
        private Character _selectedCharacter;
        private int _roundCounter;
        private ObservableCollection<Character> _charactersView;
        private Guid _viewId;
        private Character _currentPlayerWhomRoundIsNow;
        private ViewModelCommand _commandNextCharacter;
        private ViewModelCommand _commandDisableCharacter;
        private Character _previousCharacter;




        #region Public Constructor

        public ViewModelBattleWindow()
        {
            _viewId = Guid.NewGuid();
        }

        #endregion




        #region Public Properties

        public int ActiveCharactersCount
        {
            get
            {
                return Characters?.Count(x => x.IsEnabled) ?? 0;
            }
        }

        public int CurrentRoundPlayersCounter { get; set; }

        public List<Character> Characters
        {
            get { return _characters; }
            set
            {
                _characters = value;
                //if (_characters != null && _characters.Any())
                //CharactersView = new ObservableCollection<Character>(_characters);
                OnPropertyChanged();
            }
        }

        public Character CurrentPlayerWhomRoundIsNow
        {
            get { return _currentPlayerWhomRoundIsNow; }
            set
            {
                _currentPlayerWhomRoundIsNow = value;
                OnPropertyChanged();
            }
        }

        public Character PreviousCharacter
        {
            get { return _previousCharacter; }
            set
            {
                _previousCharacter = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Character> CharactersView
        {
            get { return _charactersView; }
            set
            {
                _charactersView = value;
                OnPropertyChanged();
            }
        }

        public Character SelectedCharacter
        {
            get { return _selectedCharacter; }
            set
            {
                _selectedCharacter = value;
                OnPropertyChanged();
            }
        }

        public int RoundCounter
        {
            get { return _roundCounter; }
            set
            {
                _roundCounter = value;
                OnPropertyChanged();
            }
        }

        public Guid ViewId => _viewId;

        #endregion




        #region Private Methods

        private void _SetActiveCharacter()
        {
            foreach (var character in Characters)
            {
                character.IsActiveNow = character.Id.Equals(CurrentPlayerWhomRoundIsNow.Id) && character.IsEnabled;
            }
        }

        private void _NextCharacter(object character)
        {
            if (character is Character ch) // pattern matching
            {
                CurrentRoundPlayersCounter++;
                PreviousCharacter = ch;
                if (ActiveCharactersCount == 0)
                {
                    MessageBox.Show("Nie ma już postaci zdolnych do walki");
                    WindowManager.CloseWindow(ViewId);
                    return;
                }
                if (ActiveCharactersCount <= CurrentRoundPlayersCounter)
                {
                    // new round
                    RoundCounter++;
                    CurrentRoundPlayersCounter = 0;
                    CurrentPlayerWhomRoundIsNow = Characters.First(x => x.IsEnabled);
                    SelectedCharacter = CurrentPlayerWhomRoundIsNow;
                }
                else
                {
                    CurrentPlayerWhomRoundIsNow = Characters.First(x => x.IsEnabled && x.InitiativeRoll < ch.InitiativeRoll);
                    SelectedCharacter = CurrentPlayerWhomRoundIsNow;
                }
                _SetActiveCharacter();
            }
        }

        private void _DisableCharacter(object character)
        {
            if (character is Character ch) // pattern matching
            {
                ch.IsEnabled = false;
            }
        }

        #endregion





        #region Public Commands

        public ViewModelCommand CommandNextCharacter
        {
            get { return _commandNextCharacter ?? (_commandNextCharacter = new ViewModelCommand(_NextCharacter)); }
        }

        public ViewModelCommand CommandDisableCharacter
        {
            get { return _commandDisableCharacter ?? (_commandDisableCharacter = new ViewModelCommand(_DisableCharacter)); }
        }

        #endregion 





        #region Public Methods

        public void Initialize(IEnumerable characters)
        {
            RoundCounter = 0;
            Characters = characters != null ? characters.Cast<Character>().ToList() : new List<Character>();
            if (Characters?.Any() == true)
            {
                var first = Characters.First(x => x.IsEnabled);
                CurrentPlayerWhomRoundIsNow = first;
                SelectedCharacter = first;

                _SetActiveCharacter();
            }
        }

        #endregion
    }
}
