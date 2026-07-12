using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public GameObject pausePanel; // (opsional) panel background kalau mau tampilkan menu besar

    private bool isPaused = false;

    void Start()
    {
        // Panel disembunyikan kalau tidak mau tampilan menu besar
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    // Dipanggil dari tombol UI “Restart”
    public void RestartGame()
    {
        Time.timeScale = 1f; // pastikan waktu normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Dipanggil dari tombol UI “Quit”
    public void QuitGame()
    {
        Debug.Log("Quit pressed!");
        Application.Quit();

        // Catatan: tidak keluar dari Play Mode di Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // (opsional) tombol Pause/Resume
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);
    }

    public void LoadLevel1()
    {
        Time.timeScale = 1f;      
        AudioListener.pause = false;
        SceneManager.LoadScene("Modif2");
    }

    public void LoadLevel2()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Modif2 - extended");
    }
}
