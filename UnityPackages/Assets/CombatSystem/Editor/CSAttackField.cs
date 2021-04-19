using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using CombatSystem;

public class CSAttackField : BaseField<CSAttack>
{
    public CSAttackField(string label) : base(label, new VisualElement())
    {
    }
}
