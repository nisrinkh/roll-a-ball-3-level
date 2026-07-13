using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // pastikan waktu normal lagi
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // muat ulang scene sekarang
    }

    // Fungsi keluar game
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Debug.Log("Game exited!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
