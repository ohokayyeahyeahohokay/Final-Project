using UnityEngine;

public class Slide : MonoBehaviour
{
    public float moveDistance = 3f;   
    public float moveSpeed = 2f;     

    private Vector3 startPos;
    private Vector3 leftPos;
    private Vector3 rightPos;

    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;

        leftPos = startPos - new Vector3(moveDistance, 0, 0);
        rightPos = startPos + new Vector3(moveDistance, 0, 0);
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                rightPos,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, rightPos) < 0.1f)
                movingRight = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                leftPos,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, leftPos) < 0.1f)
                movingRight = true;
        }
    }
}
