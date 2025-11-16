using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    bool canMove = true;
    public float limitLeft = -12.25f;
    public float limitRight = 12.25f;
    public Vector3 resetPosition;
    public bool isClone = false;
    public Score score;

    private int runScore = 0; // points collected during this fall

    void Start()
    {
        resetPosition = transform.position;
    }

    void Update()
    {
        if (canMove)
        {
            float moveInput = 0f;

            if (Input.GetKey(KeyCode.A))
                moveInput = -1f;
            else if (Input.GetKey(KeyCode.D))
                moveInput = 1f;

            Vector3 moveOffset = new Vector3(moveInput * moveSpeed * Time.deltaTime, 0f, 0f);
            Vector3 newPosition = transform.position + moveOffset;

            newPosition.x = Mathf.Clamp(newPosition.x, limitLeft, limitRight);

            transform.position = newPosition;

            if (Input.GetKey(KeyCode.Space))
            {
                Rigidbody rBall = GetComponent<Rigidbody>();
                canMove = false;
                rBall.isKinematic = false;
                rBall.AddForce(Random.Range(-5f, 5f), 0, 0, ForceMode.Impulse);

                runScore = 0; // reset run score when dropping
            }
        }
    }

    public void AddRunScore(int value)
    {
        runScore += value;
        Debug.Log("Run Score: " + runScore);
    }

    private void OnTriggerEnter(Collider other)
    {
        Goal goal = other.GetComponent<Goal>();
        if (goal != null)
        {
            int total = runScore * goal.multiplier;
            score.AddScore(total);

            Debug.Log($"Goal reached! RunScore={runScore}, Multiplier={goal.multiplier}, Added={total}");

            // reset for next run
            runScore = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bottom"))
        {
            if (isClone)
            {
                Destroy(gameObject);
            }
            else
            {
                canMove = true;
                Rigidbody rBall = GetComponent<Rigidbody>();
                rBall.isKinematic = true;
                transform.position = resetPosition;
                runScore = 0; // reset if ball falls without scoring
            }
        }
    }
}
