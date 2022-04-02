using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAndFire : MonoBehaviour
{
    public float arrowSpeed;
    public GameObject bowRoot;
    public GameObject arrowReleaseCenter;
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePositionWorld - bowRoot.transform.position;
        direction = new Vector3(direction.x, direction.y,0).normalized;

        bowRoot.transform.LookAt(new Vector3(mousePositionWorld.x, mousePositionWorld.y, 0));

        if(Input.GetMouseButtonDown(0))
        {
            ReleaseArrow(direction);
        }
    }


    private void ReleaseArrow(Vector3 direction)
    {
        Debug.Log(direction);

        GameObject newArrow = Instantiate(arrow, arrowReleaseCenter.transform.position, Quaternion.identity);
        newArrow.GetComponent<ArrowController>().SetDirection(direction, arrowSpeed);
    }
}
