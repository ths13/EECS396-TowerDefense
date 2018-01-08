using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    Vector3 spawnLocation;
    Object normalEnemyPrefab;
    Object fastEnemyPrefab;
    Object strongEnemyPrefab;
    public bool GameOver = false;
    public bool isLastWaveOver = false;
    // Use this for initialization
    void Start()
    {
        spawnLocation = new Vector3(0f, .65f, 0f);

        normalEnemyPrefab = Resources.Load("NormalEnemy");
        fastEnemyPrefab = Resources.Load("FastEnemy");
        strongEnemyPrefab = Resources.Load("StrongEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (isLastWaveOver)
        {
            if(FindObjectsOfType<EnemyMovement>().Length == 0)
            {
                GameOver = true;
            }
        }
    }

    public void ProcessWave(string waveTag)
    {
        switch (waveTag)
        {
            case "Wave1":
            {
                int[] waveArray = new int[] {0,0,0,0,0};
                StartCoroutine(SpawnWaves(waveArray,false));
                break;
            }
            case "Wave2":
            {
                int[] waveArray = new int[] {0,0,0,0,0,0,0,0,0,0};
                StartCoroutine(SpawnWaves(waveArray,false));
                break;
            }
            case "Wave3":
            {
                int[] waveArray = new int[] {0,0,0,0,0,1,1,1,1,1,0,0,0,0,0};
                StartCoroutine(SpawnWaves(waveArray,false));
                break;
            }
            case "Wave4":
            {
                int[] waveArray = new int[] {0,0,0,0,0,2,2,0,0,0,0,0};
                StartCoroutine(SpawnWaves(waveArray,false));
                break;
            }
            case "Wave5":
            {
                int[] waveArray = new int[] {0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,0,0,0,0};
                StartCoroutine(SpawnWaves(waveArray,false));
                break;
            }
            case "Wave6":
            {
                int[] waveArray = new int[] {0,0,0,0,0,0,0,0,0,0,2,2,2,2,1,1,1,1,1,1,1,1,1,1};
                StartCoroutine(SpawnWaves(waveArray,false));
                break;
            }
            case "Wave7":
            {
                int[] waveArray = new int[] {0,0,0,0,1,1,1,1,2,2,2,2,2,2,2,2,1,1,1,1,0,0,0,0};
                StartCoroutine(SpawnWaves(waveArray,false));
                break;
            }
            case "Wave8":
            {
                int[] waveArray = new int[] {0,1,2,0,1,2,0,1,2,0,1,2,0,1,2,0,1,2,0,1,2,0,1,2};
                StartCoroutine(SpawnWaves(waveArray,false));
                break;
            }
            case "Wave9":
            {
                int[] waveArray = new int[] {2,2,2,2,2,1,1,1,1,1,2,2,2,2,2};
                StartCoroutine(SpawnWaves(waveArray,true));
                break;
            }
        }
    }

    public IEnumerator SpawnWaves(int[] waveArray,bool isLastWave)
    {
        for(int i = 0; i < waveArray.Length; i++)
        {
            if (!GameOver)
            {
                if (waveArray[i] == 0)
                {
                    Instantiate(normalEnemyPrefab, spawnLocation, new Quaternion());
                    yield return new WaitForSeconds(2f);
                }
                else if (waveArray[i] == 1)
                {
                    Instantiate(fastEnemyPrefab, spawnLocation, new Quaternion());
                    yield return new WaitForSeconds(2f);
                }
                else if (waveArray[i] == 2)
                {
                    Instantiate(strongEnemyPrefab, spawnLocation, new Quaternion());
                    yield return new WaitForSeconds(2f);
                }
            }
        }

        if (isLastWave)
        {
            isLastWaveOver = true;
        }
    }
}
