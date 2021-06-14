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

        /// <summary>
        /// The attack stats associated with this weapon
        /// </summary>
        public CSWeaponStats Stats
        {
            get { return stats; }
        }

        private void Awake()
        {
            combo.QueueAttack += StartCurrentAttack;
        }

        /// <summary>
        /// Sends an attack to the combo tree to figure out which hitboxes to enable
        /// </summary>
        /// <param name="attack">The integer ID of the attack type to use</param>
        public void Attack(int attack)
        {
            combo.Chain(attack);
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