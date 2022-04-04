using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{

    public GameObject[] cutSceneImagesPart1;
    public GameObject[] cutSceneImagesPart2;

    private bool atPart1 = true;
    int cutsceneIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
         cutsceneIndex = 1;
        cutSceneImagesPart1[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(atPart1)
            {
                cutSceneImagesPart1[cutsceneIndex].SetActive(true);
                cutsceneIndex++;
                if(cutsceneIndex >= cutSceneImagesPart1.Length)
                {
                    atPart1 = false;
                    cutsceneIndex = 0;
                }
            }
           else
            {
                foreach(GameObject g in cutSceneImagesPart1)
                {
                    g.SetActive(false);
                }
                if(cutsceneIndex < cutSceneImagesPart2.Length)
                {
                    cutSceneImagesPart2[cutsceneIndex].SetActive(true);
                }
                cutsceneIndex++;
                if (cutsceneIndex > cutSceneImagesPart2.Length)
                {
                    UIController.Instance.FinishCutscene();
                }

            }
        }
    }
}
