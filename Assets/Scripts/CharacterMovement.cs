using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;
    Animator anim;

    float speed = 5.0f;
    float speedRotate = 100.0f;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey("w"))
        {
            anim.SetBool("isWalking", true);
            Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f * Time.deltaTime * speed);
            movement = transform.TransformDirection(movement);
            controller.Move(movement);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (Input.GetKey("a"))
        {
            Vector3 rotation = new Vector3(0.0f, -1.0f * Time.deltaTime * speedRotate, 0.0f);
            transform.Rotate(rotation);
        }
        if (Input.GetKey("s"))
        {
            Vector3 movement = new Vector3(0.0f, 0.0f, -1.0f * Time.deltaTime * speed);
            movement = transform.TransformDirection(movement);
            controller.Move(movement);
        }
        if (Input.GetKey("d"))
        {
            Vector3 rotation = new Vector3(0.0f, 1.0f * Time.deltaTime * speedRotate, 0.0f);
            transform.Rotate(rotation);
        }
    }
}