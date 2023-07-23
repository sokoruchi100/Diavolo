using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Mover mover;

        [Header("Settings")]
        [SerializeField] private float weaponRange = 2f;
        
        private Transform target;
        
        private void Update() {
            bool isInRange = Vector3.Distance(target.position, transform.position) < weaponRange;
            
            if (target != null && !isInRange) {
                mover.MoveTo(target.position);
            } else { 
                mover.Stop();
            }
        }

        public void Attack(CombatTarget combatTarget) {
            target = combatTarget.transform;
        }
    }
}