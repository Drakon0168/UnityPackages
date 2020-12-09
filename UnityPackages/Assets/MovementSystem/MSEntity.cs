using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MovementSystem;

namespace MovementSystem
{
    public class MSEntity : MSPhysicsObject
    {
        [SerializeField]
        private MSEntityStats stats;

        private bool sprinting;

        #region Accessors

        /// <summary>
        /// The current move speed of the entity (returns sprint speed if sprinting)
        /// </summary>
        public float CurrentSpeed
        {
            get
            {
                if (sprinting)
                    return stats.SprintSpeed;
                else
                    return stats.MoveSpeed;
            }
        }

        /// <summary>
        /// The movement stats of the entity
        /// </summary>
        public MSEntityStats Stats
        {
            get { return stats; }
            set { stats = value; }
        }

        /// <summary>
        /// Whether or not the unit is currently sprinting
        /// </summary>
        public bool Sprinting
        {
            get { return sprinting; }
            set { sprinting = value; }
        }

        #endregion

        #region Movement

        /// <summary>
        /// Applies a movement force in the specified direction. The movement force is specified in the attached MSEntityStats.
        /// </summary>
        /// <param name="moveDirection">The direction to move in.</param>
        public void Move(Vector3 moveDirection)
        {
            Vector3 targetVelocity = moveDirection * CurrentSpeed;
            Vector3 forceDirection = targetVelocity - Velocity;

            if (!stats.CanFly)
                forceDirection.y = 0;

            float forceLength = forceDirection.magnitude;

            if (forceLength != 0)
                forceDirection /= forceLength;

            ApplyForce(forceDirection * stats.MoveForce * (forceLength / CurrentSpeed * 2));
        }

        /// <summary>
        /// Adds an upwards velocity to the unit to allow it to jump
        /// </summary>
        /// <param name="velocityShift">Whether the velocity should be shifted by the jump velocity or set to the jump velocity, set to true to shift velocity</param>
        public void Jump(bool velocityShift = false)
        {
            if (velocityShift)
                Velocity += Vector3.up * stats.JumpVelocity;
            else
                Velocity = new Vector3(Velocity.x, stats.JumpVelocity, Velocity.z);
        }

        #endregion
    }
}