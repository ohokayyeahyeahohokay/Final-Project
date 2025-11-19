using UnityEngine;
using System.Collections;


public class TeleportPeg : MonoBehaviour
{
    public TeleportPeg targetPeg;  
    public float teleportCooldown = 0.3f;

    private void OnTriggerEnter(Collider other)   
    {
        BallControl ball = other.GetComponent<BallControl>();
        if (ball != null)
            Teleport(ball);
    }

    private void Teleport(BallControl ball)
{
    if (targetPeg == null || !ball.canTeleport) return;   

    Rigidbody rb = ball.GetComponent<Rigidbody>();
    Rigidbody2D rb2 = ball.GetComponent<Rigidbody2D>();

    ball.transform.position = targetPeg.transform.position;

    if (rb) rb.linearVelocity = Vector3.zero;
    if (rb2) rb2.linearVelocity = Vector2.zero;

    StartCoroutine(ball.DisableTeleport(teleportCooldown));
}

}
