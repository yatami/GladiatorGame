using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform Target;
    public int Health;
    public float Speed;



    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private bool AIStart;



    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        if (AIStart)
        {
            navMeshAgent.speed = Speed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(Target.position);
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
        
    }
    public void ShotEnemy()
    {
        --Health;
        if (Health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void EnemyAI(bool AI)
    {
        AIStart = AI;
    }
}
