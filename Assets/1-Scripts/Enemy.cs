using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int Health;
    private float Speed;



    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
