using UnityEngine;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [Header("Settings")]
        [SerializeField] private float chaseDistance = 5f;

        private void Update() {
            if (DistanceToPlayer() < chaseDistance) {
                Debug.Log(gameObject.name + " should chase");
            }
        }

        private float DistanceToPlayer() {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
}