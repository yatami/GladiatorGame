using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Text GoldText;
    public Text TimerText;
    public GameController gameController;
    public GameObject HomeScreenUI;
    public GameObject InGameUI;

    private int Gold = 5;
    private float StartGameTime;
    public void GameStart()
    {
        gameController.SetStage(Stage.Play);
        StartGameTime = Time.time;
        HomeScreenUI.SetActive(false);
        InGameUI.SetActive(true);
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
        GoldText.text = "$ " + Gold + "";
        if (gameController.GetStage() == Stage.Play)
        {
            TimerText.text = Mathf.Round((Time.time - StartGameTime)*100)/100  + "";
        }
        
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
