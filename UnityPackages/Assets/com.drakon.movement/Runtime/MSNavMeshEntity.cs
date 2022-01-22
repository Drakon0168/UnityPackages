using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Drakon.MovementSystem
{
    public class MSNavMeshEntity : MSEntity
    {
        private NavMeshAgent agent = null;
        private NavMeshPath path = null;
        private bool pathing = false;
        private Coroutine pathFollowRoutine = null;

        #region Accessors

        /// <summary>
        /// The nav mesh agent used by this movement component
        /// </summary>
        public NavMeshAgent Agent
        {
            get { return agent; }
            set { agent = value; }
        }

        /// <summary>
        /// The unit's currently active path
        /// </summary>
        public NavMeshPath Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// Whether or not the unit is currently going down a path
        /// </summary>
        public bool Pathing { get { return pathing; } }

        #endregion

        #region Events

        /// <summary>
        /// Called when the agent starts following a path
        /// </summary>
        public event System.Action OnPathStart;

        /// <summary>
        /// Called when the agent reaches the destination
        /// </summary>
        public event System.Action OnDestination;

        /// <summary>
        /// Called each time the agent reaches a new point on the path
        /// </summary>
        public event System.Action OnPointReached;

        #endregion

        #region Unity Methods

        protected void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updatePosition = false;
            agent.updateRotation = false;
            path = new NavMeshPath();
        }

        protected virtual void Start()
        {
            MoveToPoint(new Vector3(-10, 0, 20));
        }

        #endregion

        #region Movement

        /// <summary>
        /// Use NavMesh navigation to move towards the specified position
        /// </summary>
        /// <param name="targetPosition">The position to move towards</param>
        public virtual void MoveToPoint(Vector3 targetPosition)
        {
            agent.Warp(transform.position);
            agent.CalculatePath(targetPosition, path);
            OnPathStart?.Invoke();

            if (pathFollowRoutine != null)
            {
                StopCoroutine(pathFollowRoutine);
            }

            if (path.corners.Length > 1)
            {
                pathing = true;
                pathFollowRoutine = StartCoroutine(FollowPath());
            }
            else
            {
                OnDestination?.Invoke();
                pathing = false;
            }
        }

        /// <summary>
        /// Moves from one point along the path to the next
        /// </summary>
        /// <param name="previous">The previous point on the path</param>
        /// <param name="next">The next point on the path</param>
        private IEnumerator FollowPath()
        {
            bool reachedDestination = false;
            int[] pathIndex = new int[] { 0, 1 };
            Vector3 pathSegment = path.corners[1] - path.corners[0];
            Vector3 offset;

            while (!reachedDestination)
            {
                offset = transform.position - path.corners[pathIndex[0]];
                float projectionMult = Vector3.Dot(pathSegment, offset) / Vector3.Dot(pathSegment, pathSegment);

                Move((path.corners[pathIndex[1]] - transform.position).normalized);
                if (projectionMult >= 0.9f)
                {
                    OnPointReached?.Invoke();
                    pathIndex[0]++;
                    pathIndex[1]++;

                    if(pathIndex[1] == path.corners.Length)
                    {
                        reachedDestination = true;
                    }
                    else
                    {
                        pathSegment = path.corners[pathIndex[1]] - path.corners[pathIndex[0]];
                    }
                }

                yield return new WaitForFixedUpdate();
            }

            pathing = false;
            OnDestination?.Invoke();
        }

        #endregion
    }
}