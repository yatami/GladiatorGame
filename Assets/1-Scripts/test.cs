using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Animator animref;


    // Start is called before the first frame update
    void Start()
    {
        animref = gameObject.GetComponent<Animator>();

        animref.Play("EnemySwordDeath",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
