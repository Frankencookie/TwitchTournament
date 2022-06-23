using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour
{
    private NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>();
    }

    public void MoveToPosition(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }

    public void MoveToObject(GameObject target)
    {
        navAgent.SetDestination(target.transform.position);
    }

    public void StopMoving()
    {
        navAgent.ResetPath();
    }
}
