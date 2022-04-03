using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform Target;

    private int Health;
    private float Speed;
    private NavMeshAgent navMeshAgent;
    private bool AIStart;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        AIStart = false;
    }

    private void Update()
    {
        if (AIStart)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(Target.position);
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
        
    }
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    public void EnemyAI(bool AI)
    {
        AIStart = AI;
    }
}
