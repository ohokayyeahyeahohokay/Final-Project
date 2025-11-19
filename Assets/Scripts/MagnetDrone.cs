using UnityEngine;
using System.Collections;

public class MagnetDrone : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float stopDistance = 2f;
    public float moveSpeed = 4f;

    public float chargeTime = 1.5f;
    public float pulseForce = 20f;
    public float cooldownTime = 2f;

    private Rigidbody rb;
    private enum DroneState { Idle, Chasing, Charging, Pulsing, Cooldown }
    private DroneState state = DroneState.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch (state)
        {
            case DroneState.Idle:
                CheckForPlayer();
                break;

            case DroneState.Chasing:
                ChasePlayer();
                break;

            case DroneState.Charging:
                break; // handled in coroutine

            case DroneState.Pulsing:
                break;

            case DroneState.Cooldown:
                break;
        }
    }

    void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            state = DroneState.Chasing;
        }
    }

    void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= stopDistance)
        {
            StartCoroutine(ChargeAndPulse());
            state = DroneState.Charging;
            return;
        }

        Vector3 dir = (player.position - transform.position).normalized;
        rb.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);

        transform.forward = dir;
    }

    private IEnumerator ChargeAndPulse()
    {
        yield return new WaitForSeconds(chargeTime);

        Vector3 dir = (player.position - transform.position).normalized;

        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        playerRb.AddForce(dir * pulseForce, ForceMode.Impulse);

        state = DroneState.Pulsing;

        yield return new WaitForSeconds(0.2f);
        
        state = DroneState.Cooldown;
        yield return new WaitForSeconds(cooldownTime);

        state = DroneState.Idle;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
