using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [System.Serializable]
    public class CSAttack
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private Vector2 graphPosition;
        [SerializeReference]
        protected CSAttack[] chains = new CSAttack[0];
        [SerializeReference]
        protected CSAttack parent = null;

        #region Accessors

        /// <summary>
        /// The list of attacks that this attack can chain to
        /// </summary>
        public CSAttack[] Chains
        {
            get { return chains; }
        }

        /// <summary>
        /// The number of attacks that this attack can possibly chain to
        /// </summary>
        public int ChainCount
        {
            get { return Chains.Length; }
            set
            {
                chains = new CSAttack[value];
            }
        }

        /// <summary>
        /// The attack that this attack chains from
        /// </summary>
        public CSAttack Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// The name of the attack as seen in the graph
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The position of this attack's node in the graph view
        /// </summary>
        public Vector2 GraphPosition
        {
            get { return graphPosition; }
            set { graphPosition = value; }
        }

        #endregion

        public CSAttack(string name)
        {
            this.name = name;
        }
    }
}