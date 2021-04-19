using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace CombatSystem {
    public class CSComboNode : Node
    {
        public string GUID;
        public CSAttack attack = null;
        public CSAttack[] attackChains = new CSAttack[0];
        public bool entryPoint = false;
    }
}