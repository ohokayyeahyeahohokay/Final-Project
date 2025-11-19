using UnityEngine;
public class KeyPickup : MonoBehaviour
{
    MeshRenderer rend;
    Collider col;

    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    public void HideKey()
    {
        rend.enabled = false;
        col.enabled = false;
    }

    public void ResetKey()
    {
        rend.enabled = true;
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        BallControl ball = other.GetComponent<BallControl>();
        if (ball == null) return;

        ball.hasKey = true;
        HideKey();
    }
}
