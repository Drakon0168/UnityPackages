using System.Collections.Generic;
using Drakon.CombatSystem;
using UnityEngine;

namespace Drakon.CombatSystem
{
    public class CSWeapon : MonoBehaviour
    {
        [SerializeField]
        private List<CSAttackStats> attacks;

        public CSAttackStats Root
        {
            get
            {
                if (attacks == null)
                {
                    attacks = new List<CSAttackStats> {new CSAttackStats("Idle")};
                }

                return attacks[0];
            }
            set
            {
                if (attacks == null)
                {
                    attacks = new List<CSAttackStats> {new CSAttackStats("Idle")};
                }

                attacks[0] = value;
            }
        }
    }
}