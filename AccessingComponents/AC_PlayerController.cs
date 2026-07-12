using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class AC_PlayerController : MonoBehaviour
{
    // === Komponen dasar ===
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    [Header("Referensi")]
    public GameObject Setting;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // === Lompatan ===
    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float gravityScale = 2f;
    private bool isGrounded = true;

    Vector3 velocity;


    // === Audio ===
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip pickupSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    // === Timer ===
    [Header("Timer Settings")]
    public TextMeshProUGUI timerText;
    public float timeRemaining = 30f;
    private bool timerIsRunning = true;

    // === UI Buttons ===
    [Header("UI Elements")]
    public GameObject restartButton;
    public GameObject quitButton;

    // === Nyawa / Heart System ===
    [Header("Health Settings")]
    public int playerLives = 3;
    public HeartDisplay heartDisplay;

    [Header("Collision Effect")]
    public GameObject collisionEffectPrefab;

    // === Posisi awal player ===
    private Vector3 startPosition;

    // === PAUSE ===
    private bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        // Sistem heart
        if (heartDisplay != null)
            heartDisplay.UpdateHearts(playerLives);

        // Posisi awal
        startPosition = transform.position;

        // Tombol UI disembunyikan di awal
        restartButton.SetActive(false);
        quitButton.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Update()
    {
        if (!isPaused && timerIsRunning)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
            {
                Jump();
                Debug.Log("Space key pressed! Initiating jump.");
            }
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * gravityScale);
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
            if (jumpSound != null) audioSource.PlayOneShot(jumpSound);
            isGrounded = false;
        }
    }



    void FixedUpdate()
    {
        // Kalau game di-pause atau waktu habis, jangan jalan
        if (isPaused || !timerIsRunning) return;

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        float playerSpeed = Setting.GetComponent<AC_SettingController>().playerSpeed;
        rb.AddForce(movement * playerSpeed);

        // Timer tetap berjalan seperti biasa
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                TimeUp();
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }

    }

    // === Pick Up ===
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

            if (pickupSound != null)
                audioSource.PlayOneShot(pickupSound);
        }
    }

    // === Update skor & kemenangan ===
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 7)
        {
            winTextObject.SetActive(true);
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            timerIsRunning = false;

            if (winSound != null) audioSource.PlayOneShot(winSound);

            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null) Destroy(enemy);

            GameObject bgMusic = GameObject.Find("BackgroundMusic");
            if (bgMusic != null) bgMusic.GetComponent<AudioSource>().Stop();
        }
    }

    // === Tabarakan dengan musuh ===
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Efek tabrakan
            if (collisionEffectPrefab != null)
            {
                Instantiate(
                    collisionEffectPrefab,
                    collision.contacts[0].point,          // posisi tabrakan
                    Quaternion.identity                   // tanpa rotasi khusus
                );
            }

            playerLives--;
            if (heartDisplay != null)
                heartDisplay.UpdateHearts(playerLives);

            if (playerLives > 0)
            {
                transform.position = startPosition;
                Debug.Log("Kena musuh! Nyawa tersisa: " + playerLives);
                if (loseSound != null) audioSource.PlayOneShot(loseSound);
            }
            else
            {
                timerIsRunning = false;
                LoseGame();
            }
        }

        // Deteksi tanah untuk lompat
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            Debug.Log("Grounded on: " + collision.gameObject.name);
        }
    }

    // === Timer habis ===
    void TimeUp()
    {
        timerIsRunning = false;
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "Time’s Up!";

        GameObject bgMusic = GameObject.Find("BackgroundMusic");
        if (bgMusic != null)
            bgMusic.GetComponent<AudioSource>().Stop();

        if (loseSound != null)
            audioSource.PlayOneShot(loseSound);

        restartButton.SetActive(true);
        quitButton.SetActive(true);

        Time.timeScale = 0f;
    }

    // === Kalah ===
    void LoseGame()
    {
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

        GameObject bgMusic = GameObject.Find("BackgroundMusic");
        if (bgMusic != null)
            bgMusic.GetComponent<AudioSource>().Stop();
        if (loseSound != null) audioSource.PlayOneShot(loseSound);

        restartButton.SetActive(true);
        quitButton.SetActive(true);

        Time.timeScale = 0f;
    }


    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0) timeToDisplay = 0;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    // === Fungsi tombol ===
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game closed."); // hanya terlihat di Editor
    }

    // === Pause Manual ===
    public void TogglePause()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
