using UnityEngine;

namespace RPG.Combat {
    public class Health : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Animator animator;

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

            animator.SetTrigger("die");
            isDead = true;
        }
    }
}
