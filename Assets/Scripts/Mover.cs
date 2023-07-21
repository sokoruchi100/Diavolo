using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

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
            agent.destination = hit.point;
        }
    }
}
