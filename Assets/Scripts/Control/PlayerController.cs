using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Mover mover;
        [SerializeField] private Fighter fighter;
        [SerializeField] private Health health;

        private void Update() {
            if (health.IsDead()) { return; }

            if (InteractWithCombat()) { return; }

            if (InteractWithMovement()) { return; }
        }

        private bool InteractWithCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits) {
                if (!hit.transform.TryGetComponent(out CombatTarget target)) { continue; }

                if (!fighter.CanAttack(target.gameObject)) { continue; }

                if (Input.GetMouseButton(0)) {
                    fighter.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement() {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit) {
                if (Input.GetMouseButton(0)) {
                    mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}