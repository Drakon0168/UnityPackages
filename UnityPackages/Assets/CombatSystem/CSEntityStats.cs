using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(fileName = "NewCombatEntityStats", menuName = "CombatSystem/CSEntityStats")]
    public class CSEntityStats : ScriptableObject
    {
        [Header("Health")]
        [Tooltip("The maximum amount of health that this entity can have.")]
        [SerializeField]
        private float maxHealth = 100.0f;
        [Tooltip("Whether or not health should be able to regenerate. True to enable health regen.")]
        [SerializeField]
        private bool healthRegenEnabled = false;
        [Tooltip("The amount of health to regenerate per second when health regen starts.")]
        [SerializeField]
        private float healthRegen = 5.0f;
        [Tooltip("The amount of time to wait before starting health regeneration after taking damage.")]
        [SerializeField]
        private float healthRegenTime = 5.0f;

        [Header("Shields")]
        [Tooltip("Whether or not to enable shields.")]
        [SerializeField]
        private bool shieldsEnabled = true;
        [Tooltip("The maximum shields that this entity can have.")]
        [SerializeField]
        private float maxShield = 50.0f;
        [Tooltip("The rate at which shields regenerate per second when shield regen starts.")]
        [SerializeField]
        private float shieldRegen = 20.0f;
        [Tooltip("The amount of time to wait before starting shield regen after taking damage.")]
        [SerializeField]
        private float shieldRegenTime = 2.5f;
    }
}