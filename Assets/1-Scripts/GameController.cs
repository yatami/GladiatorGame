using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    List<GameObject> Enemys;
    Stage stage;
    private float spawnRateMin;
    private float spawnRateMax;

    [System.Serializable] public class StartGameEvent : UnityEvent { }
    StartGameEvent startGameEvent = new StartGameEvent();
    [System.Serializable] public class ResetGameEvent : UnityEvent { }
    ResetGameEvent resetGameEvent = new ResetGameEvent();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        stage = Stage.UI;
        Enemys = new List<GameObject>();
        spawnRateMin = 2;
        spawnRateMax = 4;
        StartCoroutine(SpawnEnemy());
        startGameEvent.AddListener(StartGame);
        resetGameEvent.AddListener(ResetGame);
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (stage == Stage.Play)
            {
                yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));
                Debug.Log("EnemySpawn");
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (startGameEvent != null)
                resetGameEvent.Invoke();
        }
    }

    private void GameReset()
    {
        stage = Stage.UI;
        UIController.Instance.HomeScreenUI.SetActive(true);
        UIController.Instance.InGameUI.SetActive(false);
    }

    public void SetStage(Stage stage)
    {
        this.stage = stage;
    }

    public Stage GetStage()
    {
        return stage;
    }

    private void StartGame()
    {
        UIController.Instance.GameStart();
    }

    private void ResetGame()
    {
        GameReset();
    }

    public void StartGameButton()
    {
        if (startGameEvent != null)
            startGameEvent.Invoke();
    }
}
