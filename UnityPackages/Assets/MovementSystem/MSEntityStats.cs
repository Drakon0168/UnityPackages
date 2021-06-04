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
        [Tooltip("Whether or not to change speed based on the direction of motion.")]
        [SerializeField]
        private bool variableSpeed;
        [Tooltip("Sideways movement multiplier. Only applicable with variable move speed.")]
        [SerializeField]
        private float strafeSpeedMult = 0.75f;
        [Tooltip("Backwards speed multiplier. Only applicable with variable move speed.")]
        [SerializeField]
        private float backSpeedMult = 0.25f;
        [Tooltip("The maximum angle from the transform's forward to count as forward movement in degrees.")]
        [SerializeField]
        private float forwardAngle = 60.0f;
        [Tooltip("The maximum angle from the transform's back to count as forward movement in degrees.")]
        [SerializeField]
        private float backwardsAngle = 30.0f;
        [Tooltip("The speed that the entity moves at while sprinting.")]
        [SerializeField]
        private float sprintSpeed = 7.5f;
        [Tooltip("The target time for it to take the entity to move at its desired velocity.")]
        [SerializeField]
        private float accelerationTime = 0.2f;
        [Tooltip("The amount of force to apply when moving the entity. Lower values may cause the entity to drift, set to 100 by default.")]
        [SerializeField]
        private float moveForce = 100.0f;
        [Tooltip("The amount of force to apply when jumping.")]
        [SerializeField]
        private float jumpVelocity = 5.0f;
        [Tooltip("Whether or not the entity can fly, if true the entity can apply move forces in the y axis.")]
        [SerializeField]
        private bool canFly = false;
        [Tooltip("The default speed of the dash.")]
        [SerializeField]
        private float dashSpeed = 20.0f;
        [Tooltip("The amount of time taken to dash.")]
        [SerializeField]
        private float dashTime = 0.2f;
        [Tooltip("Whether or not the dash speed changes over the course of a dash. Toggle on to enable dash speed curve.")]
        [SerializeField]
        private bool variableDashSpeed = true;
        [Tooltip("Curve indicating the speed of the dash over its length. X of 0 is the start of the dash, X of 1 is the end, Y of 1 is dash speed, Y of 0 means there is no movement.")]
        [SerializeField]
        private AnimationCurve dashSpeedOverTime;

        #region Accessors

        /// <summary>
        /// The speed that the entity moves at by default.
        /// </summary>
        public float MoveSpeed
        {
            get { return moveSpeed; }
        }

        /// <summary>
        /// Sideways movement speed. Only applicable with variable move speed.
        /// </summary>
        public bool VariableMoveSpeed
        {
            get { return variableSpeed; }
        }

        /// <summary>
        /// The speed multiplier to apply when straffing. Only applicable with variable move speed.
        /// </summary>
        public float StrafeSpeedMult
        {
            get { return strafeSpeedMult; }
        }

        /// <summary>
        /// The speed multiplier to apply when backpedaling. Only applicable with variable move speed.
        /// </summary>
        public float BackSpeedMult
        {
            get { return backSpeedMult; }
        }

        /// <summary>
        /// The maximum angle from the transform's forward to count as forward movement in degrees.
        /// </summary>
        public float ForwardAngle
        {
            get { return forwardAngle; }
        }

        /// <summary>
        /// The maximum angle from the transform's back to count as forward movement in degrees.
        /// </summary>
        public float BackAngle
        {
            get { return backwardsAngle; }
        }

        /// <summary>
        /// The speed that the entity moves at while sprinting.
        /// </summary>
        public float SprintSpeed
        {
            get { return sprintSpeed; }
        }

        /// <summary>
        /// The target time for it to take the entity to move at its desired velocity.
        /// </summary>
        public float AccelerationTime
        {
            get { return accelerationTime; }
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

        /// <summary>
        /// The default speed of the dash.
        /// </summary>
        public float DashSpeed
        {
            get { return dashSpeed; }
        }

        /// <summary>
        /// The amount of time taken to dash.
        /// </summary>
        public float DashTime
        {
            get { return dashTime; }
        }

        /// <summary>
        /// Whether or not the dash speed changes over the course of a dash. Toggle on to enable dash speed curve.
        /// </summary>
        public bool VariableDashSpeed
        {
            get { return variableDashSpeed; }
        }

        /// <summary>
        /// Curve indicating the speed of the dash over its length. X of 0 is the start of the dash, X of 1 is the end, Y of 1 is dash speed, Y of 0 means there is no movement.
        /// </summary>
        public AnimationCurve DashSpeedOverTime
        {
            get { return dashSpeedOverTime; }
        }

        #endregion
    }
}