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
        
        private Health target;
        private float timeSinceLastAttack = 0f;
        
        private void Update() {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) { return; }

            if (target.IsDead()) { return; }

            if (!GetIsInRange()) {
                mover.MoveTo(target.transform.position);
            } else {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour() {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack >= timeBetweenAttacks) {
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack() {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        public void Hit() {
            if (target == null) { return; }

            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange() {
            return Vector3.Distance(target.transform.position, transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget) {
            actionScheduler.StartAction(this);
            target = combatTarget.transform.GetComponent<Health>();
        }

        public void Cancel() {
            StopAttack();
            target = null;
        }

        private void StopAttack() {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget) {
            return !combatTarget.GetComponent<Health>().IsDead();
        }
    }
}