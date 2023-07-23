using UnityEngine;
using RPG.Movement;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private Mover mover;

        private void Update() {
            if (Input.GetMouseButton(0)) {
                MoveToCursor();
            }
        }

        private void MoveToCursor() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit) {
                mover.MoveTo(hit.point);
            }
        }
    }
}