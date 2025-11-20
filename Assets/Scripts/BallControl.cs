using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class BallControl : MonoBehaviour
{
    //movement
    public float moveSpeed = 3f;
    public float dashForce = 7f;
    public float jumpForce = 10f;

    bool canMove = true;
    public float limitLeft = -12.25f;
    public float limitRight = 12.25f;

    //ball state
    public Vector3 resetPosition;
    public bool isClone = false;

    //scoring
    public Score score;
    private int runScore = 0;
    public int currentBallPlayer = 1;
    public int player1Score = 0;
    public int player2Score = 0;
    public TextMeshProUGUI player1ScoreTextHud;
    public TextMeshProUGUI player2ScoreTextHud;
    public GameObject scorepanel;

    //gameover
    public GameObject gameOverPanel;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;



    //UI
    public TextMeshProUGUI roundText;
    public int roundCounter = 1;
    public int maxRounds = 2;

    private bool gameEnded = false;
    public PlayerCharges playerCharges;


    Rigidbody rb;

    //portal peg
    public bool canTeleport = true;

    public bool hasKey = false;

    void Start()
    {
        resetPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        UpdateScorePanels();
        UpdateRoundUI();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded)
            return;

        if (canMove)
        {
            TopMovement();
            Drop();
        }
        else
        {
            HandleDashAndJump();
        }
    }

    //top movement before dropping
    void TopMovement()
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

    //game start drop
    void Drop()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            canMove = false;
            rb.isKinematic = false;

            rb.AddForce(Random.Range(-5f, 5f), 0, 0, ForceMode.Impulse);

            runScore = 0;
        }
    }

    //mid-air movement
    void HandleDashAndJump()
    {
        if (!canMove)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (playerCharges.UseCharge())
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (playerCharges.UseCharge())
                    rb.AddForce(Vector3.left * dashForce, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (playerCharges.UseCharge())
                    rb.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.W))
            {
                if (playerCharges.UseCharge())
                {
                    Vector3 curve = (Vector3.up + Vector3.left).normalized;
                    rb.AddForce(curve * jumpForce, ForceMode.Impulse);
                }
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.W))
            {
                if (playerCharges.UseCharge())
                {
                    Vector3 curve = (Vector3.up + Vector3.right).normalized;
                    rb.AddForce(curve * jumpForce, ForceMode.Impulse);
                }
            }
        }
    }

    //collect points during fall
    public void AddRunScore(int value)
    {
        runScore += value;

        if (currentBallPlayer == 1)
            player1Score += value;
        else
            player2Score += value;

        UpdateScorePanels();

    
        score.AddScore(value);
    }

    void UpdateScorePanels()
    {
        player1ScoreTextHud.text = "Player 1: " + player1Score;
        player2ScoreTextHud.text = "Player 2: " + player2Score;
    }



    //goal scoring
    private void OnTriggerEnter(Collider other)
    {
        Goal goal = other.GetComponent<Goal>();

        if (goal != null)
        {
            int bonus = runScore * (goal.multiplier - 1);

            if (score != null && bonus > 0)
                score.AddScore(bonus);

            Debug.Log($"Goal reached! RunScore={runScore}, Multiplier={goal.multiplier}x, Bonus={bonus}");
            Debug.Log("Main Score: " + score.score);

            runScore = 0;
        }
    }

    //bottom collision 
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bottom"))
            return;

        if (isClone)
        {
            Destroy(gameObject);
            return;
        }

        roundCounter++;
        UpdateRoundUI();

        if (roundCounter > 2)
        {
            EndGame();
            return;
        }


        ResetBall();
        SwitchPlayers();

    }

    void ResetBall()
    {
        canMove = true;
        rb.isKinematic = true;
        transform.position = resetPosition;
        runScore = 0;
        hasKey = false;
        FindObjectOfType<KeyPickup>()?.ResetKey();
        FindObjectOfType<UnlockPeg>()?.ResetLock();
        
    }

    void SwitchPlayers()
    {
        if (currentBallPlayer == 1)
            currentBallPlayer = 2;
        else
            currentBallPlayer = 1;

        UpdateScorePanels();

        Debug.Log("Now it's Player " + currentBallPlayer + "'s turn as the Ball.");
    }


    void UpdateRoundUI()
    {
        roundText.text = "Round: " + roundCounter + "/" + maxRounds;
    }

    public IEnumerator DisableTeleport(float time)
    {
        canTeleport = false;
        yield return new WaitForSeconds(time);
        canTeleport = true;
    }



    void EndGame()
    {
        gameEnded = true;

        Debug.Log("ROUND OVER");

        // freeze game
        Time.timeScale = 0f;

        string winner = (player1Score > player2Score) ? "PLAYER 1 WINS!" : "PLAYER 2 WINS!";
        winnerText.text = winner;

        // update score display
        player1ScoreText.text = "P1 Score: " + player1Score;
        player2ScoreText.text = "P2 Score: " + player2Score;

        // show panel
        scorepanel.SetActive(false);
        gameOverPanel.SetActive(true);

    }
}
