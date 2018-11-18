using System;

namespace InitiativeRoller.Models
{
    public class Character
    {
        public Character(string name, int initiativeModifier, int initiativeRoll)
        {
            Name = name;
            InitiativeModificator = initiativeModifier;
            AssignInitiative(initiativeRoll);
            IsActiveNow = false;
            IsEnabled = true;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; private set; }
        public int InitiativeModificator { get; private set; }
        public int InitiativeRoll { get; private set; }

        public bool IsActiveNow { get; set; }
        public bool IsEnabled { get; set; }

        private void AssignInitiative(int input)
        {
            InitiativeRoll = input + InitiativeModificator;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
