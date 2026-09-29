using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Transform player;
    private Rigidbody rb;
    private EnemySpawner spawner;

    [SerializeField] private int health = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = Random.Range(2.5f, 4.5f);

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
            player = playerObject.transform;
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        Vector3 direction = player.position - transform.position;

        direction.y = 0f;

        if (direction.magnitude < 0.1f)
            return;

        direction.Normalize();

        rb.MovePosition(
            rb.position +
            direction * speed * Time.fixedDeltaTime
        );
    }

    private bool canDamage = true;

    private void OnCollisionStay(Collision collision)
    {
        if (!canDamage)
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        canDamage = false;

        GameManager.Instance.LoseLife();

        Invoke(nameof(ResetDamage), 1.5f);
    }

    private void ResetDamage()
    {
        canDamage = true;
    }
    public void SetSpawner(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.EnemyDestroyed();
        }
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}