using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private Mover mover;
        [SerializeField] private Fighter fighter;

        private void Update() {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits) {
                if (!hit.transform.TryGetComponent(out CombatTarget target)) { continue; }

                if (Input.GetMouseButtonDown(0)) {
                    fighter.Attack(target);
                }
            }
        }

        private void InteractWithMovement() {
            if (Input.GetMouseButton(0)) {
                MoveToCursor();
            }
        }

        private void MoveToCursor() {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit) {
                mover.MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}