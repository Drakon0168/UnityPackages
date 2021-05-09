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
        [SerializeField]
        private string[] chains;

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

        #endregion
    }
}