using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball;      
    public float speed = 5f;
    public float offsetY = 3f;   

    void LateUpdate()
    {
       
        Vector3 pos = transform.position;

        float targetY = ball.position.y + offsetY;

        pos.y = Mathf.Lerp(pos.y, targetY, speed * Time.deltaTime);

        transform.position = pos;
    }
}
