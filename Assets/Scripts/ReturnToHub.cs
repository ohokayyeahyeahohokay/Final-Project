using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToHub : MonoBehaviour
{
    public void GoToHub()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Hub"); 
    }
}
