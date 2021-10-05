using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drakon.CombatSystem
{
    public class CSWeapon : MonoBehaviour
    {
        [SerializeReference]
        private List<CSAttack> attacks = new List<CSAttack>();
        private string[] attackTypes;

        #region Accessors

        public CSAttack Root
        {
            get
            {
                if(attacks.Count == 0)
                {
                    attacks.Add(new CSAttack("Root"));
                }

                return attacks[0];
            }
            set { attacks[0] = value; }
        }

        #endregion
    }
}