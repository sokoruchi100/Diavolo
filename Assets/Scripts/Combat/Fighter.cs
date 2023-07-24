using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {
        [Header("References")]
        [SerializeField] private Mover mover;
        [SerializeField] private ActionScheduler actionScheduler;

        [Header("Settings")]
        [SerializeField] private float weaponRange = 2f;
        
        private Transform target;
        
        private void Update() {
            if (target == null) { return; }

            if (!GetIsInRange()) {
                mover.MoveTo(target.position);
            } else {
                mover.Cancel();
            }
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