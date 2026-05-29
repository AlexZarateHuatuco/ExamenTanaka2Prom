using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    public void SpawnEnemy()
    {
        int randomIndex =Random.Range(0, spawnPoints.Length);
        Transform spawnPoint =spawnPoints[randomIndex];

        if (!Physics.CheckSphere(spawnPoint.position, 1f))
        {
            Instantiate(enemyPrefab,spawnPoint.position,spawnPoint.rotation);
        }
    }
}