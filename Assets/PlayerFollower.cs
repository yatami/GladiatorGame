using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{

    GameObject playerRef;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        startPos = playerRef.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = (playerRef.transform.position - startPos)/5;
    }
}
