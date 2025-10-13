using UnityEngine;

public class BottomBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clone"))
        {
            Destroy(other.gameObject);
        }
    }
}
