using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Dash")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private LayerMask obstacleLayer;

    private Rigidbody rb;

    private Vector3 movement;
    private Vector3 lastDirection;

    private float dashTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadInput();

        dashTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryDash();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(
            rb.position +
            movement * moveSpeed * Time.fixedDeltaTime
        );
    }

    private void ReadInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(
            horizontal,
            0f,
            vertical
        ).normalized;

        if (movement != Vector3.zero)
        {
            lastDirection = movement;
        }
    }

    private void TryDash()
    {
        if (dashTimer > 0f)
            return;

        if (lastDirection == Vector3.zero)
            return;

        float distance = dashDistance;

        if (Physics.SphereCast(
            rb.position,
            0.45f,
            lastDirection,
            out RaycastHit hit,
            dashDistance,
            obstacleLayer))
        {
            distance = hit.distance - 0.5f;
        }

        if (distance <= 0f)
            return;

        Vector3 dashPosition =
            rb.position + lastDirection * distance;

        rb.MovePosition(dashPosition);

        dashTimer = dashCooldown;
    }

    public Vector3 GetLastDirection()
    {
        return lastDirection;
    }
}