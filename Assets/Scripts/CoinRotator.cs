using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 5;

    private void OnTriggerEnter(Collider other)
    {
        BallControl ball = other.GetComponent<BallControl>();
        ball.AddRunScore(coinValue);
        Destroy(gameObject);
    }
}
