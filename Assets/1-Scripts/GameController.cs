using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{


    List<GameObject> Enemys;
    Stage stage;
    private float spawnRateMin;
    private float spawnRateMax;
    public UIController UIController;

    private void Start()
    {
        stage = Stage.UI;
        Enemys = new List<GameObject>();
        spawnRateMin = 2;
        spawnRateMax = 4;
        StartCoroutine(SpawnEnemy());
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
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameReset();
        }
    }

    private void GameReset()
    {
        stage = Stage.UI;
        UIController.HomeScreenUI.SetActive(true);
        UIController.InGameUI.SetActive(false);
    }

    public void SetStage(Stage stage)
    {
        this.stage = stage;
    }
    public Stage GetStage()
    {
        return stage;
    }
}
