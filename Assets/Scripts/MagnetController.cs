using UnityEngine;

public class MagnetPulse : MonoBehaviour
{
    public MagnetCharges chargeUI;  
    public float moveSpeed = 15f;
    public float depth = 0f;
    public float pulseRadius = 5f;
    public float pulseStrength = 500f; 
    public AudioSource pulse;

    public Rigidbody ballRb;

    private Camera main;

    void Start()
    {
        main = Camera.main;
    }

    void Update()
    {
        MoveWithMouse();

        if (Input.GetKeyDown(KeyCode.UpArrow))
            Pulse(true);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Pulse(false);
    }

    void MoveWithMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(main.transform.position.z - depth);

        Vector3 worldPos = main.ScreenToWorldPoint(mousePos);

        transform.position = Vector3.Lerp(transform.position, worldPos, moveSpeed * Time.deltaTime);
    }

    void Pulse(bool push)
    {
        if (!chargeUI.UseCharge())
        {
            Debug.Log("No magnet charges left!");
            return;
        }
    
        Vector3 direction = (ballRb.position - transform.position);
        float distance = direction.magnitude;

        pulse.Play();

        
        if (distance <= pulseRadius)
        {
            direction.Normalize();
            if (!push)
                direction = -direction;

            ballRb.AddForce(direction * pulseStrength);
        }
    }

    //this is used to see the pulse radius when not playing a scene, we can remove this later
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pulseRadius);
    }
}
