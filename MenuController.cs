using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Dipanggil oleh tombol Play
    public void PlayGame()
    {
        SceneManager.LoadScene("Opening");
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    // Dipanggil oleh tombol Quit
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
