using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject collectiblePrefab;

    [SerializeField] private int collectibleAmount = 10;

    [SerializeField] private float spawnRange = 20f;

    [SerializeField] private LayerMask obstacleLayer;

    private void Start()
    {
        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    {
        for (int i = 0; i < collectibleAmount; i++)
        {
            SpawnCollectible();
        }
    }

    public void SpawnCollectible()
    {
        Vector3 position;

        int attempts = 0;

        do
        {
            position = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0.5f,
                Random.Range(-spawnRange, spawnRange)
            );

            attempts++;
        }
        while (
            Physics.CheckSphere(position, 0.5f, obstacleLayer)
            && attempts < 20
        );

        GameObject collectible = Instantiate(
            collectiblePrefab,
            position,
            Quaternion.identity
        );

        Collectible collectibleScript =
            collectible.GetComponent<Collectible>();

        if (collectibleScript != null)
        {
            collectibleScript.SetSpawner(this);
        }
    }
}