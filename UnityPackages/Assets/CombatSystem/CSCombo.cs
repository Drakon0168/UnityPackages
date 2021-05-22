using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(fileName = "New Combo", menuName = "CombatSystem/Combo")]
    public class CSCombo : ScriptableObject
    {
        [HideInInspector]
        [SerializeReference]
        private List<CSAttack> attacks;
        private CSAttack activeAttack = null;
        private CSAttack nextAttack = null;
        [SerializeField]
        private string[] chains;

        #region Callbacks

        /// <summary>
        /// Called when the current attack is first started.
        /// </summary>
        public event AttackTimer WindupStart;

        /// <summary>
        /// Called when the current attack becomes active, this is when hitboxes will be activated
        /// </summary>
        public event AttackTimer AttackStart;

        /// <summary>
        /// Called at the end of the active time of the current attack this is when hitboxes should be disabled
        /// </summary>
        public event AttackTimer AttackEnd;

        /// <summary>
        /// Called at the end of the cooldown period when the current attack ends this is when new actions can be taken again
        /// </summary>
        public event AttackTimer CooldownEnd;

        /// <summary>
        /// Called when the combo timer for the current attack is finished this is the point to stop listening for new combo input
        /// </summary>
        public event AttackTimer ComboEnd;

        #endregion

        #region Accessors

        /// <summary>
        /// The full list of attacks in this combo tree
        /// </summary>
        private List<CSAttack> Attacks
        {
            get
            {
                if(attacks == null || attacks.Count == 0)
                {
                    attacks = new List<CSAttack>();
                    attacks.Add(new CSAttack("Entry"));
                    attacks[0].ChainCount = Chains.Length;
                }

                return attacks;
            }
        }

        /// <summary>
        /// The idle point of the combat tree
        /// </summary>
        public CSAttack Entry
        {
            get
            {
                return Attacks[0];
            }
            set
            {
                Attacks[0] = value;
            }
        }

        /// <summary>
        /// The available actions to use when chaining attacks
        /// </summary>
        public string[] Chains
        {
            get { return chains; }
        }

        #endregion

        #region Attack Management

        /// <summary>
        /// Adds an attack to the attack list 
        /// </summary>
        /// <param name="attack">The attack to add</param>
        /// <param name="parent">The attack to chain from</param>
        /// <param name="chain">The chain index to attach the new attack to</param>
        public void AddAttack(CSAttack attack, CSAttack parent, int chain)
        {
            attack.ChainCount = Chains.Length;
            attacks.Add(attack);

            if (parent != null)
            {
                if (parent.Chains[chain] != null)
                {
                    RemoveAttack(parent.Chains[chain]);
                }

                parent.Chains[chain] = attack;
                attack.Parent = parent;
            }
        }

        /// <summary>
        /// Removes the specified attack and any chains from the combo tree
        /// </summary>
        /// <param name="attack">The attack to remove</param>
        public void RemoveAttack(CSAttack attack)
        {
            for (int i = 0; i < attack.ChainCount; i++)
            {
                if (attack.Chains[i] != null)
                {
                    RemoveAttack(attack.Chains[i]);
                }
            }

            if (attack.Parent != null)
            {
                for (int i = 0; i < attack.Parent.ChainCount; i++)
                {
                    if (attack.Parent.Chains[i] == attack)
                    {
                        attack.Parent.Chains[i] = null;
                        break;
                    }
                }

                attack.Parent = null;
            }

            attacks.Remove(attack);
        }

        /// <summary>
        /// Resets the combo to its idle state
        /// </summary>
        public void Reset()
        {
            SwitchAttack(Entry);
        }

        /// <summary>
        /// Sets the next attack to move to when the current attack finishes
        /// </summary>
        /// <param name="input">The index of the input to use in the Chains list</param>
        public void Chain(int input)
        {
            if(activeAttack.Chains[input] != null)
            {
                nextAttack = activeAttack.Chains[input];
            }
        }

        /// <summary>
        /// Switches the active attack to the given attack
        /// </summary>
        /// <param name="attack">The attack to switch to</param>
        private void SwitchAttack(CSAttack attack)
        {
            activeAttack.WindupStart -= OnWindup;
            activeAttack.AttackStart -= OnAttackStart;
            activeAttack.AttackEnd -= OnAttackEnd;
            activeAttack.CooldownEnd -= OnCooldownEnd;
            activeAttack.ComboEnd -= OnComboEnd;

            activeAttack = attack;
            nextAttack = null;

            activeAttack.WindupStart += OnWindup;
            activeAttack.AttackStart += OnAttackStart;
            activeAttack.AttackEnd += OnAttackEnd;
            activeAttack.CooldownEnd += OnCooldownEnd;
            activeAttack.ComboEnd += OnComboEnd;
        }

        #endregion

        #region Callbacks

        private void OnWindup()
        {
            WindupStart();
        }

        private void OnAttackStart()
        {
            AttackStart();
        }

        private void OnAttackEnd()
        {
            AttackEnd();
        }

        private void OnCooldownEnd()
        {
            CooldownEnd();

            if(nextAttack != null)
            {
                nextAttack.Attack();
            }
        }

        private void OnComboEnd()
        {
            ComboEnd();
            Reset();
        }

        #endregion
    }
}