using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem {
    public class CSWeapon : MonoBehaviour
    {
        [SerializeField]
        private CSCombo combo;
        [SerializeField]
        private CSWeaponStats stats;
        [SerializeField]
        private string[] attackTypes;

        public CSCombo Combo
        {
            get
            {
                if(combo == null)
                {
                    combo = new CSCombo(attackTypes, new List<CSAttack>());
                }

                return combo;
            }
        }

        /// <summary>
        /// The attack stats associated with this weapon
        /// </summary>
        public CSWeaponStats Stats
        {
            get { return stats; }
        }

        public string[] Chains
        {
            get { return attackTypes; }
        }

        private void Awake()
        {
            Combo.QueueAttack += StartCurrentAttack;
        }

        /// <summary>
        /// Sends an attack to the combo tree to figure out which hitboxes to enable
        /// </summary>
        /// <param name="attack">The integer ID of the attack type to use</param>
        public void Attack(int attack)
        {
            Combo.Chain(attack);
        }

        private void StartCurrentAttack()
        {
            if (combo.ActiveAttack != combo.Entry)
            {
                StartCoroutine(combo.ActiveAttack.Attack());
            }
        }
    }
}