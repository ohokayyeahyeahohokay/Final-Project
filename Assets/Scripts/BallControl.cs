using UnityEngine;

public class BallControl : MonoBehaviour
{
    //movement
    public float moveSpeed = 5f;
    public float dashForce = 10f;
    public float jumpForce = 12f;

    bool canMove = true;
    public float limitLeft = -12.25f;
    public float limitRight = 12.25f;

    //ball state
    public Vector3 resetPosition;
    public bool isClone = false;

    //scoring
    public Score score;
    private int runScore = 0; // points collected during this fall

    Rigidbody rb;

    void Start()
    {
        resetPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canMove)
        {
            HandleTopMovement();
            HandleDrop();
        }
        else
        {
            HandleDashAndJump();
        }
    }

    //ball movement function when it hasnt been dropped
    void HandleTopMovement()
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
    }

    //drop function
    void HandleDrop()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            canMove = false;
            rb.isKinematic = false;

            // slight starting push
            rb.AddForce(Random.Range(-5f, 5f), 0, 0, ForceMode.Impulse);

            runScore = 0;
        }
    }

    //jump and dash
    void HandleDashAndJump()
    {
        if (!canMove)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                rb.AddForce(Vector3.left * dashForce, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                rb.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.W))
            {
                Vector3 curve = (Vector3.up + Vector3.left).normalized;
                rb.AddForce(curve * jumpForce, ForceMode.Impulse);
            }

            
            if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.W))
            {
                Vector3 curve = (Vector3.up + Vector3.right).normalized;
                rb.AddForce(curve * jumpForce, ForceMode.Impulse);
            }
        }
    }

    //score add function
    public void AddRunScore(int value)
    {
        runScore += value;
        // Add points immediately to the main score so UI updates right away
        if (score != null)
        {
            score.AddScore(value);
        }
        Debug.Log("Run Score: " + runScore);
    }


    private void OnTriggerEnter(Collider other)
{
    Goal goal = other.GetComponent<Goal>();
    if (goal != null)
    {
        // Calculate bonus: since coins already added points, give bonus based on multiplier
        // Bonus = runScore * (multiplier - 1) so 2x gives 1x bonus, 3x gives 2x bonus, etc.
        int bonus = runScore * (goal.multiplier - 1);
        if (score != null && bonus > 0)
        {
            score.AddScore(bonus);
        }

        Debug.Log($"Goal reached! RunScore={runScore}, Multiplier={goal.multiplier}x, Bonus={bonus}");
        Debug.Log("Main Score: " + score.score);

        runScore = 0;
    }
}


    //reset ball function
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
                rb.isKinematic = true;
                transform.position = resetPosition;
                runScore = 0;
            }
        }
    }
}
