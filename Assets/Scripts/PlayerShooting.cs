using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Disparo")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootCooldown = 0.3f;

    private PlayerController playerController;
    private float cooldownTimer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        UpdateFirePointDirection();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void UpdateFirePointDirection()
    {
        Vector3 direction = playerController.GetLastDirection();

        if (direction == Vector3.zero)
            return;

        firePoint.forward = direction;
    }

    private void Shoot()
    {
        if (cooldownTimer > 0f)
            return;

        Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        cooldownTimer = shootCooldown;
    }
}