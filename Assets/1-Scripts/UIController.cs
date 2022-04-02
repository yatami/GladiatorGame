using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Text Text;
    //Game Controller çek
    private int Gold = 5;
    public GameController gameController;
    public void GameStart()
    {
        //Oyunu baþlat
        gameController.SetStagePlay();
    }
    public void GameQuit() 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    private void Update()
    {
        Text.text = "$ " + Gold + "";
    }

    public void AddGold(int otherGold)
    {
        Gold += otherGold;
    }

    public int GetGold()
    {
        return Gold;
    }

}
