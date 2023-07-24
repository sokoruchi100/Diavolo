using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Mover mover;
        [SerializeField] private Fighter fighter;

        private void Update() {
            if (InteractWithCombat()) { return; }

            if (InteractWithMovement()) { return; }

            Debug.Log("DOING NOTHING");
        }

        private bool InteractWithCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits) {
                if (!hit.transform.TryGetComponent(out CombatTarget target)) { continue; }

                if (!fighter.CanAttack(target.gameObject)) { continue; }

                if (Input.GetMouseButtonDown(0)) {
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