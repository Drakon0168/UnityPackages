using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class MSPhysicsObject : MonoBehaviour
    {
        private new Rigidbody rigidbody;

        private Vector3 acceleration;

        #region Accessors

        /// <summary>
        /// The current position of this object
        /// </summary>
        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        /// <summary>
        /// The current velocity of the object
        /// </summary>
        public Vector3 Velocity
        {
            get { return rigidbody.velocity; }
            set { rigidbody.velocity = value; }
        }

        /// <summary>
        /// The current acceleration of the object
        /// </summary>
        public Vector3 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        /// <summary>
        /// The mass of the object
        /// </summary>
        public float Mass
        {
            get { return rigidbody.mass; }
        }

        #endregion

        #region Unity Functions

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Velocity += acceleration * Time.deltaTime;
            Acceleration = Vector3.zero;
        }

        #endregion

        #region Physics

        /// <summary>
        /// Applies the specified force to the object.
        /// </summary>
        /// <param name="force">The force to apply</param>
        /// <param name="affectedByMass">Whether or not the force takes the object's mass into account, true by default</param>
        public void ApplyForce(Vector3 force, bool affectedByMass = true)
        {
            if (affectedByMass)
                force /= Mass;

            Acceleration += force;
        }

        #endregion
    }
}