using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem {
    public class CSWeapon : MonoBehaviour
    {
        [SerializeField]
        private List<CSAttack> attacks;
        [SerializeField]
        [Tooltip("The number of possible actions available to this weapon, used to decide which attacks to chain into, for example a weapon with only light or heavy attacks would have a combo inputs value of 2.")]
        private int comboInputs;

        private CSAttack currentAttack;
        private CSAttack nextAttack;

        void Awake()
        {
            ResetCombo();
        }

        #region Combo Management

        public void Chain()
        {
            if(nextAttack != null)
            {
                nextAttack.Attack();
                nextAttack.OnAttackEnd = Chain;

                nextAttack.OnAttackStart = OnAttackStart;
                nextAttack.OnAttackEnd += OnAttackEnd;

                currentAttack = nextAttack;
                nextAttack = null;
            }
            else
            {
                ResetCombo();
            }
        }

        /// <summary>
        /// Selects the next attack based on the given input
        /// </summary>
        /// <param name="input">The input that decides which attack to chain to, should be a value from 0 to comboInputs</param>
        public void Combo(int input)
        {
            //TODO: Set this up to use a tree structure once the editor window is complete
            nextAttack = attacks[input];
        }

        /// <summary>
        /// Resets the combo to the initial state
        /// </summary>
        public void ResetCombo()
        {
            currentAttack = attacks[0];
            nextAttack = null;
        }

        #endregion

        #region Listeners

        public void OnAttackStart()
        {
            Debug.Log("Attack Started.");
        }

        public void OnAttackEnd()
        {
            Debug.Log("Attack Ended");
        }

        #endregion
    }
}