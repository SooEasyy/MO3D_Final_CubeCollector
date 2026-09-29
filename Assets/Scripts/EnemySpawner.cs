using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn")]
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private float spawnRange = 20f;

    [Header("Cantidad")]
    [SerializeField] private int maxEnemies = 5;

    private int currentEnemies = 0;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnTime);
    }

    private void SpawnEnemy()
    {
        if (currentEnemies >= maxEnemies)
            return;

        Vector3 position;

        do
        {
            position = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0.5f,
                Random.Range(-spawnRange, spawnRange)
            );
        }
        while (Vector3.Distance(position, Vector3.zero) < 8f);

        GameObject enemy = Instantiate(
            enemyPrefab,
            position,
            Quaternion.identity
        );

        currentEnemies++;

        Enemy enemyScript = enemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.SetSpawner(this);
        }
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}