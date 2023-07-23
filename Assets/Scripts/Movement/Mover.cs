using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
    public class Mover : MonoBehaviour {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;

        private void Update() {
            UpdateAnimator();
        }

        private void UpdateAnimator() {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        public void MoveTo(Vector3 destination) {
            agent.destination = destination;
        }
    }
}