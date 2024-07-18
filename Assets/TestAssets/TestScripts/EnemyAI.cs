using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void UpdateAI()
    {
        agent.SetDestination(target.position);
    }
}
