using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
    public class Mover : MonoBehaviour {
        [Header("References")]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private ActionScheduler actionScheduler;
        [SerializeField] private Fighter fighter;

        private void Update() {
            UpdateAnimator();
        }

        private void UpdateAnimator() {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        public void StartMoveAction(Vector3 destination) {
            actionScheduler.StartAction(this);
            fighter.Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination) {
            agent.destination = destination;
            agent.isStopped = false;
        }

        public void Stop() {
            agent.isStopped = true;
        }
    }
}