using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 5; // how much each coin is worth

    private void OnTriggerEnter(Collider other)
    {
        BallControl ball = other.GetComponent<BallControl>();
        if (ball != null)
        {
            ball.AddRunScore(coinValue);
            Destroy(gameObject); // remove the coin when collected
        }
    }
}
