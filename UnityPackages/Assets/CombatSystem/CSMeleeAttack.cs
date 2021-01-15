using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

namespace CombatSystem
{
    public class CSMeleeAttack : CSAttack
    {
        [Header("Attack Timing Stats")]
        [Tooltip("The time it takes to begin an attack, the hitbox will not activate before this time has passed.")]
        [SerializeField]
        private float anticipation;
        [Tooltip("The time the hitbox is active and can damage entities inside of it.")]
        [SerializeField]
        private float attackTime;
        [Tooltip("The amount of time after the attack that the entity cannot perform another action.")]
        [SerializeField]
        private float recovery;
        [Tooltip("The amount of time after the attack that the entity has to initiate a combo.")]
        [SerializeField]
        private float comboTime;

        [Header("Hitboxes")]
        [Tooltip("The list of colliders to use when attacking with this attack.")]
        [SerializeField]
        private List<Collider> colliders;

        //Attack events
        private OnAttack attackAnticipation;
        private OnAttack attackRecovery;
        private OnAttack attackComboEnd;

        #region Accessors

        /// <summary>
        /// The time the hitbox is active and can damage entities inside of it
        /// </summary>
        public float Anticipation
        {
            get { return anticipation; }
            set { anticipation = value; }
        }

        /// <summary>
        /// The time the hitbox is active and can damage entities inside of it
        /// </summary>
        public float AttackTime
        {
            get { return attackTime; }
            set { attackTime = value; }
        }

        /// <summary>
        /// The amount of time after the attack that the entity cannot perform another action
        /// </summary>
        public float Recovery
        {
            get { return recovery; }
            set { recovery = value; }
        }

        /// <summary>
        /// The amount of time after the attack that the entity has to initiate a combo
        /// </summary>
        public float ComboTime
        {
            get { return comboTime; }
            set { comboTime = value; }
        }

        /// <summary>
        /// OnAttack event called when the anticipation time starts
        /// </summary>
        public OnAttack OnAnticipation
        {
            get { return attackAnticipation; }
            set { attackAnticipation = value; }
        }

        /// <summary>
        /// OnAttack event called when the attack recovery time begins 
        /// </summary>
        public OnAttack OnRecovery
        {
            get { return attackRecovery; }
            set { attackRecovery = value; }
        }

        /// <summary>
        /// OnAttack event called when the attack combo time ends
        /// </summary>
        public OnAttack OnComboEnd
        {
            get { return attackComboEnd; }
            set { attackComboEnd = value; }
        }

        #endregion

        #region Attack

        public override void Attack()
        {
            StartCoroutine(BeginAttack());
        }

        /// <summary>
        /// Initiates an attack taking the attack timers into account
        /// </summary>
        /// <returns></returns>
        IEnumerator BeginAttack()
        {
            if (attackAnticipation != null)
                attackAnticipation();

            yield return new WaitForSeconds(anticipation);

            if (attackStart != null)
                attackStart();

            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }

            yield return new WaitForSeconds(attackTime);

            if (attackRecovery != null)
                attackRecovery();

            foreach(Collider collider in colliders)
            {
                collider.enabled = false;
            }

            yield return new WaitForSeconds(recovery);

            if (attackEnd != null)
                attackEnd();

            yield return new WaitForSeconds(comboTime);

            if (attackComboEnd != null)
                attackComboEnd();
        }

        #endregion
    }
}