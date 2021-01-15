using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public delegate void OnAttack();
    public delegate void OnHit(List<CSEntity> targets);

    public abstract class CSAttack : MonoBehaviour
    {
        protected OnAttack attackStart;
        protected OnAttack attackEnd;
        protected OnHit onHit;

        #region Accessors

        /// <summary>
        /// OnAttack event called when the anticipation time has ended and the attack time begins
        /// </summary>
        public OnAttack OnAttackStart
        {
            get { return attackStart; }
            set { attackStart = value; }
        }

        /// <summary>
        /// OnAttack event called when the recovery time has ended and either the next attack starts or the combo ends
        /// </summary>
        public OnAttack OnAttackEnd
        {
            get { return attackEnd; }
            set { attackEnd = value; }
        }

        /// <summary>
        /// OnHit event called when any CSEntities are hit by the attack. Parameters are a list of CSEntities that have been hit by the attack
        /// </summary>
        public OnHit OnHit
        {
            get { return onHit; }
            set { onHit = value; }
        }

        #endregion

        /// <summary>
        /// Initiates an attack
        /// </summary>
        public abstract void Attack();
    }
}