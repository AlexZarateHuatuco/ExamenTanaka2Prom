using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private int currentWave = 1;
    [SerializeField] private float timeBetweenWaves = 5f;

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        int enemiesToSpawn = currentWave * 3;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            spawner.SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;
        StartCoroutine(StartWave());
    }
}