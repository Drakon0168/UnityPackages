using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drakon.CombatSystem
{
    [System.Serializable]
    public class CSAttack
    {
        [SerializeField]
        private float windupTime = 0.0f;
        [SerializeField]
        private float attackTime = 0.0f;
        [SerializeField]
        private float cooldownTime = 0.0f;
        [SerializeField]
        private float comboTime = 0.0f;

        [SerializeField]
        private string name = "";
        [SerializeField]
        private Collider collider = null;
        [SerializeReference]
        private List<CSAttack> children = new List<CSAttack>();
        [SerializeReference]
        private CSAttack parent = null;
        [SerializeField]
        private int attackIndex = 0;

        #region Accessors

        /// <summary>
        /// The time taken from the start of the attack to when the hitbox becomes active
        /// </summary>
        public float WindupTime
        {
            get { return windupTime; }
        }

        /// <summary>
        /// The time that the hitbox remains active during the attack
        /// </summary>
        public float AttackTime
        {
            get { return attackTime; }
        }

        /// <summary>
        /// The time after the end of the attack where no other action can be taken
        /// </summary>
        public float CooldownTime
        {
            get { return cooldownTime; }
        }

        /// <summary>
        /// The time after the cooldown where this attack can be combo'd into another attack
        /// </summary>
        public float ComboTime
        {
            get { return comboTime; }
        }

        /// <summary>
        /// The display name of the attack
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The collider to associate with this attack
        /// </summary>
        public Collider Collider
        {
            get { return collider; }
        }

        /// <summary>
        /// The attacks that can be chained to from this attack
        /// </summary>
        public List<CSAttack> Children
        {
            get { return children; }
            set { children = value; }
        }

        /// <summary>
        /// The attack that this attack was chained from
        /// </summary>
        public CSAttack Parent
        {
            get { return parent; }
            set 
            {
                if (parent != null && parent.Children != null)
                {
                    parent.children.RemoveAt(attackIndex);
                }

                parent = value;
                parent.Children[attackIndex] = this;
            }
        }

        /// <summary>
        /// The index in the parent's children list that refers to this attack. Typically this number refers to the input used to trigger the attack
        /// </summary>
        public int AttackIndex
        {
            get { return attackIndex; }
            set 
            {
                if(parent != null && parent.Children != null && parent.Children[attackIndex] == this)
                {
                    parent.Children[attackIndex] = null;
                    parent.Children[value] = this;
                }
                attackIndex = value;
            }
        }

        #endregion

        #region Constructor

        public CSAttack(string name, CSAttack parent = null, int attackIndex = 0)
        {
            this.name = name;
            this.parent = parent;
            this.attackIndex = attackIndex;
        }

        #endregion
    }
}