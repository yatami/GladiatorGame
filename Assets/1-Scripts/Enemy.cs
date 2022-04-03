using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform Target;
    public int Health;
    public float Speed;
    EnemyAnimationController animationController;

    private Vector3 I_EnemytoTarget;
    private float I_DotUp;
    private float I_DotRight;
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private bool AIStart;



    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        animationController = GetComponent<EnemyAnimationController>();
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
        DirEnemy();
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

    private void DirEnemy()
    {
        I_EnemytoTarget = Target.position - transform.position;
        I_DotUp = Vector3.Dot(Vector3.up,I_EnemytoTarget);
        if (I_DotUp > 0)
        {
            animationController.ChangeFrontBack(false);
        }
        else
        {
            animationController.ChangeFrontBack(true);
        }
        I_DotRight = Vector3.Dot(Vector3.right, I_EnemytoTarget);
        if (I_DotRight > 0)
        {
            animationController.ChangeSides(true);
        }
        else
        {
            animationController.ChangeSides(false);
        }
    }
}
