using UnityEngine;

public class PickupRotator : MonoBehaviour
{


    public GameObject ballInScene; 
    public int multiplyCount = 5;
    public float spread = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("entered");
        if (other.CompareTag("Ball"))
        {
            MultiplyBalls(other.gameObject);
            Destroy(gameObject);
        }
    }

    void MultiplyBalls(GameObject originalBall)
    {
        for (int i = 0; i < multiplyCount; i++)
        {
            Vector3 spawnPos = originalBall.transform.position +
                               Random.insideUnitSphere * spread;


            GameObject clone = Instantiate(ballInScene, spawnPos, Quaternion.identity);
            clone.tag = "Ball";

            BallControl ballCtrl = clone.GetComponent<BallControl>();
            if (ballCtrl != null)
                ballCtrl.isClone = true;

            Rigidbody origRb = originalBall.GetComponent<Rigidbody>();
            Rigidbody cloneRb = clone.GetComponent<Rigidbody>();
            if (origRb && cloneRb)
                cloneRb.linearVelocity = origRb.linearVelocity;
        }
    }
    

}
