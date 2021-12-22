using System;
using System.Collections.Generic;
using UnityEngine;

namespace Drakon.CombatSystem
{
    [Serializable]
    public class CSAttackStats
    {
        [SerializeField] 
        private string name;
        [SerializeField] 
        private float windupTime;
        [SerializeField] 
        private float attackTime;
        [SerializeField] 
        private float cooldownTime;
        [SerializeField] 
        private float comboTime;

        [SerializeField]
        private Vector2 gridPosition;
        [SerializeReference]
        private CSAttackStats parent;
        [SerializeReference]
        private List<CSAttackStats> children;

        /// <summary>
        /// The name of this attack
        /// </summary>
        public string Name => name;

        /// <summary>
        /// The time it takes to activate the attack from the moment the attack is started
        /// </summary>
        public float WindupTime => windupTime;

        /// <summary>
        /// The active time of the attack 
        /// </summary>
        public float AttackTime => attackTime;

        /// <summary>
        /// The time after the active part of the attack before other actions can be taken
        /// </summary>
        public float CooldownTime => cooldownTime;

        /// <summary>
        /// The time 
        /// </summary>
        public float ComboTime => comboTime;

        public CSAttackStats(string name)
        {
            this.name = name;
            windupTime = 0;
            attackTime = 0;
            cooldownTime = 0;
            comboTime = 0;

            gridPosition = Vector2.zero;
            children = new List<CSAttackStats>();
        }
    }
}