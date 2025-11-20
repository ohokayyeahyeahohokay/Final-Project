using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 5;
    public AudioSource coinPickup;
    MeshRenderer rend;
    Collider col;


    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    public void HideCoin()
    {
        rend.enabled = false;
        col.enabled = false;
    }

    public void ResetCoin()
    {
        rend.enabled = true;
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        BallControl ball = other.GetComponent<BallControl>();
        ball.AddRunScore(coinValue);
        HideCoin();
        coinPickup.Play();
    }
}
