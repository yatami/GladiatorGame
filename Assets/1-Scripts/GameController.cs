using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    enum Stage
    {
        UI,
        Play,
        Reset
    }
    List<GameObject> Enemys;
    Stage stage;
    private float spawnRate;
    private void Start()
    {
        stage = Stage.UI;
        Enemys = new List<GameObject>();
        spawnRate = 2;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (stage == Stage.Play)
            {
                Debug.Log("EnemySpawn");
                //spawn object
                yield return new WaitForSeconds(spawnRate);
            }
            yield return null;
        }
    }

    public void SetStagePlay()
    {
        stage = Stage.Play;
    }

}
