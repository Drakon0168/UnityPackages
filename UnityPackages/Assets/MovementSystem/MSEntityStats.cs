using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    [CreateAssetMenu(fileName = "NewMovementEntityStats", menuName = "MovementSystem/MSEntityStats")]
    public class MSEntityStats : ScriptableObject
    {
        [Header("Movement Stats")]
        [Tooltip("The speed that the entity moves at by default.")]
        [SerializeField]
        private float moveSpeed = 2.5f;
        [Tooltip("The speed that the entity moves at while sprinting.")]
        [SerializeField]
        private float sprintSpeed = 7.5f;
        [Tooltip("The amount of force to apply when moving the entity. Lower values may cause the entity to drift, set to 100 by default.")]
        [SerializeField]
        private float moveForce = 100.0f;
        [Tooltip("The amount of force to apply when jumping.")]
        [SerializeField]
        private float jumpVelocity = 5.0f;
        [Tooltip("Whether or not the entity can fly, if true the entity can apply move forces in the y axis.")]
        private bool canFly = false;

        #region Accessors

        /// <summary>
        /// The speed that the entity moves at by default.
        /// </summary>
        public float MoveSpeed
        {
            get { return moveSpeed; }
        }

        /// <summary>
        /// The speed that the entity moves at while sprinting.
        /// </summary>
        public float SprintSpeed
        {
            get { return sprintSpeed; }
        }

        /// <summary>
        /// The amount of force to apply when moving the entity.
        /// </summary>
        public float MoveForce
        {
            get { return moveForce; }
        }

        /// <summary>
        /// The upwards velocity of the unit when it jumps.
        /// </summary>
        public float JumpVelocity
        {
            get { return jumpVelocity; }
        }

        /// <summary>
        /// Whether or not the entity can fly, if true the entity can apply move forces in the y axis.
        /// </summary>
        public bool CanFly
        {
            get { return canFly; }
        }

        #endregion
    }
}