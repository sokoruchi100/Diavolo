using UnityEngine;

namespace RPG.Core {
    public class Health : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private ActionScheduler actionScheduler;

        [Header("Settings")]
        [SerializeField] private float healthPoints = 100f;
        
        private bool isDead = false;

        public bool IsDead() { 
            return isDead;
        }

        public void TakeDamage(float damage) {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0) {
                Die();
            }
        }

        private void Die() {
            if (isDead) { return; }

            isDead = true;
            animator.SetTrigger("die");
            actionScheduler.CancelCurrentAction();
        }
    }
}
