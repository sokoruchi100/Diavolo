using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Fighter fighter;
        [SerializeField] private Health health;
        [SerializeField] private Mover mover;
        [SerializeField] private ActionScheduler actionScheduler;

        [Header("Settings")]
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 5f;

        private GameObject player;
        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start() {
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
        }

        private void Update() {
            if (health.IsDead()) { return; }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player)) {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            } else if (timeSinceLastSawPlayer < suspicionTime) {
                SuspicionBehaviour();
            } else {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour() {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour() {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour() {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer() {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //Called by Unity
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}