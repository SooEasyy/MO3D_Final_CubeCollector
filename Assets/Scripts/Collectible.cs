using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int points = 1;

    private CollectibleSpawner spawner;

    public void SetSpawner(CollectibleSpawner collectibleSpawner)
    {
        spawner = collectibleSpawner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.AddScore(points);

        if (spawner != null)
        {
            spawner.SpawnCollectible();
        }

        Destroy(gameObject);
    }
}