using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractToLoadBoard : MonoBehaviour
{
    [Header("Interaction")]
    bool playerInRange = false;

    [Header("UI Prompt")]
    public GameObject promptUI; // e.g. "Press E to play" text

    [Header("Glow / Highlight")]
    public Renderer buttonRenderer;
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        // Hide the prompt at start
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }

        // Cache renderer if not assigned
        if (buttonRenderer == null)
        {
            buttonRenderer = GetComponent<Renderer>();
        }

        // Set initial color
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = normalColor;
        }
    }

    void Update()
    {
        // If player is near and presses E, load BoardLevel
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("BoardLevel");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing entering has the PlayerMovement script
        if (other.GetComponent<PlayerMovement>() != null)
        {
            playerInRange = true;

            // 1) Show "Press E" text
            if (promptUI != null)
            {
                promptUI.SetActive(true);
            }

            // 2) Change color to highlight
            if (buttonRenderer != null)
            {
                buttonRenderer.material.color = highlightColor;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            playerInRange = false;

            // Hide prompt again
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }

            // Return to normal color
            if (buttonRenderer != null)
            {
                buttonRenderer.material.color = normalColor;
            }
        }
    }
}
