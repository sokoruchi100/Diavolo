using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Fighter fighter;
        [SerializeField] private Health health;

        [Header("Settings")]
        [SerializeField] private float chaseDistance = 5f;

        private GameObject player;

        private void Start() {
            player = GameObject.FindWithTag("Player");
        }

        private void Update() {
            if (health.IsDead()) { return; }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player)) {
                fighter.Attack(player);
            } else { 
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer() {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}