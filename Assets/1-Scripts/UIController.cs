using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Text GoldText;
    public Text TimerText;
    public GameObject HomeScreenUI;
    public GameObject InGameUI;
    public static UIController Instance;

    private int Gold = 0;
    private float StartGameTime;
    

    private void Awake()
    {
        Instance = this;
    }

    public void GameStart()
    {
        GameController.Instance.SetStage(Stage.Play);
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
        if (GameController.Instance.GetStage() == Stage.Play)
        {
            float x = Mathf.Round((Time.time - StartGameTime) * 100);
            if ( x % 3f == 0)
            {
                TimerText.text = x / 100 + "";
            }
            
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
