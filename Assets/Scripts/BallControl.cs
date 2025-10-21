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
            {
                moveInput = -1f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveInput = 1f;
            }

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

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bottom"))
        {
            if (isClone)
            {
                // destroy clones
                Destroy(gameObject);
            }
            else
            {
                // reset original ball
                canMove = true;
                Rigidbody rBall = GetComponent<Rigidbody>();
                rBall.isKinematic = true;
                transform.position = resetPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Goal goal = other.GetComponent<Goal>();
        if (goal != null)
        {
            score.AddScore(goal.points);
        }
    }
}
