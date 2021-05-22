using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public delegate void AttackTimer();

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

        protected float windupTime;
        protected float attackTime;
        protected float cooldownTime;
        protected float comboTime;

        #region Callbacks

        /// <summary>
        /// Called when the attack is first started.
        /// </summary>
        public event AttackTimer WindupStart;

        /// <summary>
        /// Called when the attack becomes active, this is when hitboxes will be activated
        /// </summary>
        public event AttackTimer AttackStart;

        /// <summary>
        /// Called at the end of the active time of the attack this is when hitboxes should be disabled
        /// </summary>
        public event AttackTimer AttackEnd;

        /// <summary>
        /// Called at the end of the cooldown period when the attack ends this is when new actions can be taken again
        /// </summary>
        public event AttackTimer CooldownEnd;

        /// <summary>
        /// Called when the combo timer is finished this is the point to stop listening for new combo input
        /// </summary>
        public event AttackTimer ComboEnd;

        #endregion

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

        /// <summary>
        /// The time to wait between when the attack is started and when it actually takes effect, this is the time between the WindupStart and Attack start events
        /// </summary>
        public float WindupTime
        {
            get { return windupTime; }
            set { windupTime = value; }
        }

        /// <summary>
        /// The time for the attack to remain active this is the time between the AttackStart and AttackEnd events
        /// </summary>
        public float AttackTime
        {
            get { return attackTime; }
            set { attackTime = value; }
        }

        /// <summary>
        /// The time after the end of the attack when additional actions cannot be taken, this is the time between the AttackEnd and CooldownEnd events
        /// </summary>
        public float CooldownTime
        {
            get { return cooldownTime; }
            set { cooldownTime = value; }
        }

        /// <summary>
        /// The amount of time to wait after the end of this attack when combos can be continued this is the time between the CooldownEnd and ComboEnd events
        /// </summary>
        public float ComboTime
        {
            get { return comboTime; }
            set { comboTime = value; }
        }

        #endregion

        public CSAttack(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Starts the attack playing it's timers
        /// </summary>
        public IEnumerator Attack()
        {
            WindupStart();

            yield return new WaitForSeconds(windupTime);

            AttackStart();

            yield return new WaitForSeconds(attackTime);

            AttackEnd();

            yield return new WaitForSeconds(cooldownTime);

            CooldownEnd();

            yield return new WaitForSeconds(comboTime);

            ComboEnd();
        }
    }
}