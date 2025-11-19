using UnityEngine;

public class UnlockPeg : MonoBehaviour
{
    public float triggerDistance = 2f;

    private MeshRenderer rend;
    private Collider col;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        BallControl ball = FindObjectOfType<BallControl>();
        if (ball == null) return;

        float distance = Vector3.Distance(ball.transform.position, transform.position);

        if (distance < triggerDistance && ball.hasKey)
        {
            // hide the lock object
            rend.enabled = false;
            col.enabled = false;
        }
        else
        {
            // show the lock again
            rend.enabled = true;
            col.enabled = true;
        }
    }

    public void ResetLock()
    {
        rend.enabled = true;
        col.enabled = true;
    }
}
