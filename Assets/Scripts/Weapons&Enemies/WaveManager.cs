using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private int currentWave = 1;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int maxWaves = 3;
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            StartCoroutine(StartWave());

            Debug.Log("Oleadas iniciadas");
        }
    }

    private IEnumerator StartWave()
    {
        int enemiesToSpawn = currentWave * 3;

        Debug.Log("Iniciando Wave " + currentWave);
        Debug.Log("Enemigos a generar: " + enemiesToSpawn);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Debug.Log("Spawn enemigo #" + i);

            spawner.SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;

        if (currentWave > maxWaves)
        {
            Debug.Log("Todas las oleadas completadas");
            yield break;
        }
        StartCoroutine(StartWave());
    }
}