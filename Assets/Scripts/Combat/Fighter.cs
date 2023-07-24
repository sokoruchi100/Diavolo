using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {
        [Header("References")]
        [SerializeField] private Mover mover;
        [SerializeField] private ActionScheduler actionScheduler;
        [SerializeField] private Animator animator;

        [Header("Settings")]
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 10f;
        
        private Transform target;
        private float timeSinceLastAttack = 0f;
        
        private void Update() {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) { return; }

            if (!GetIsInRange()) {
                mover.MoveTo(target.position);
            } else {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour() {
            if (timeSinceLastAttack >= timeBetweenAttacks) {
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        public void Hit() {
            Health targetHealth = target.GetComponent<Health>();
            targetHealth.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange() {
            return Vector3.Distance(target.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel() {
            target = null;
        }
    }
}