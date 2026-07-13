using UnityEngine;
using UnityEngine.UI;
using TMPro; // hanya jika kamu ingin ubah teks
using UnityEngine.SceneManagement;

public class PauseButtonController : MonoBehaviour
{
    [Header("References")]
    public Image pauseIcon;       // drag Image component (PauseButtonImage)
    public Sprite pauseSprite;    // pause icon sprite
    public Sprite playSprite;     // play icon sprite
    public GameObject pauseOverlay; // optional: panel UI yang muncul saat pause (mis. "PAUSED")
    public TextMeshProUGUI pauseText; // optional: teks yang tampil saat pause

    private bool isPaused = false;

    void Start()
    {
        // Pastikan icon awal sesuai status (game running)
        if (pauseIcon != null && pauseSprite != null)
            pauseIcon.sprite = pauseSprite;

        if (pauseOverlay != null)
            pauseOverlay.SetActive(false);
    }

    // Method ini bisa dipanggil dari OnClick() Button atau dipanggil manual dari script lain
    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        if (pauseIcon != null && playSprite != null)
            pauseIcon.sprite = playSprite;

        if (pauseOverlay != null)
            pauseOverlay.SetActive(true);

        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (pauseIcon != null && pauseSprite != null)
            pauseIcon.sprite = pauseSprite;

        if (pauseOverlay != null)
            pauseOverlay.SetActive(false);

        AudioListener.pause = false;
    }

    // Optional: restart current scene
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        // Boleh juga toggle pakai tombol ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
