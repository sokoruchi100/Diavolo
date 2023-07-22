using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    private void Update() {
        if (Input.GetMouseButton(0)) {
            MoveToCursor();
        }
        UpdateAnimator();
    }

    private void UpdateAnimator() {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);
    }

    private void MoveToCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit) {
            agent.destination = hit.point;
        }
    }
}
