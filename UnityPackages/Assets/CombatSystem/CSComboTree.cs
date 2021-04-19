using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(fileName = "NewCombo", menuName = "CombatSystem/Combo")]
    public class CSComboTree : ScriptableObject
    {
        private struct CSComboState
        {
            public CSAttack attack;
            public List<CSComboState> chains;
            public Vector2Int nodePosition;
        }
        
        [SerializeField]
        [Tooltip("The names of the different types of input that can be taken in by this combo. (Light Attack, Heavy Attack, Special, etc)")]
        private string[] inputNames;
        [SerializeField]
        [Tooltip("A list of all of the attacks that can be performed in this tree.")]
        private List<CSAttack> attacks;
        private List<CSComboState> states;
        private CSComboState currentState;
        
        public CSAttack CurrentAttack
        {
            get { return currentState.attack; }
        }

        public string[] InputNames
        {
            get { return inputNames; }
        }

        /// <summary>
        /// Moves from this state to the next one based on the given input
        /// </summary>
        /// <param name="input">The input used to determine which state to move to</param>
        /// <returns>The attack used by the next state</returns>
        public CSAttack NextAttack(int input)
        {
            currentState = currentState.chains[input];
            return currentState.attack;
        }

        /// <summary>
        /// Resets the combo to the default state
        /// </summary>
        public void ResetCombo()
        {
            currentState = states[0];
        }
    }
}