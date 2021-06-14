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
        private Vector2 graphPosition;
        [SerializeReference]
        protected CSAttack[] chains = new CSAttack[0];
        [SerializeReference]
        protected CSAttack parent = null;

        [SerializeField]
        protected float damageMult;
        [SerializeReference]
        protected Collider collider;
        [SerializeField]
        protected float windupTime;
        [SerializeField]
        protected float attackTime;
        [SerializeField]
        protected float cooldownTime;
        [SerializeField]
        protected float comboTime;

        protected bool attacking = false;
        protected bool canCombo = false;

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
        /// The collider to use as this attack's hitbox
        /// </summary>
        public Collider Collider
        {
            get { return collider; }
            set { collider = value; }
        }

        /// <summary>
        /// The multiplier to apply to the weapon's base damage
        /// </summary>
        public float DamageMult
        {
            get { return damageMult; }
            set { damageMult = value; }
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

        /// <summary>
        /// Whether or not the attack is currently in the process of attacking
        /// </summary>
        public bool Attacking
        {
            get { return attacking; }
        }

        /// <summary>
        /// Whether or not the attack is still within the combo window
        /// </summary>
        public bool CanCombo
        {
            get { return canCombo; }
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
            WindupStart?.Invoke();
            attacking = true;
            canCombo = true;
            Debug.Log($"{Name} is in windup");

            yield return new WaitForSeconds(windupTime);

            AttackStart?.Invoke();
            if (collider != null)
            {
                collider.enabled = true;
            }
            Debug.Log($"{Name} is attacking");

            yield return new WaitForSeconds(attackTime);

            AttackEnd?.Invoke();
            if (collider != null)
            {
                collider.enabled = false;
            }
            Debug.Log($"{Name} is in cooldown");

            yield return new WaitForSeconds(cooldownTime);

            CooldownEnd?.Invoke();
            attacking = false;
            Debug.Log($"{Name} is in combo time");

            yield return new WaitForSeconds(comboTime);

            ComboEnd?.Invoke();
            canCombo = false;
            Debug.Log($"{Name} can no longer combo");
        }
    }
}