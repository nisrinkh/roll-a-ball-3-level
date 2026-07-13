using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Ganti dengan nama scene level utama kamu
        SceneManager.LoadScene("Modif A");
    }

    public void OpenSettings()
    {
        // Aktifkan panel settings
        GameObject settingsPanel = GameObject.Find("SettingsPanel");
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void OpenHelp()
    {
        GameObject helpPanel = GameObject.Find("HelpPanel");
        if (helpPanel != null)
            helpPanel.SetActive(true);
    }

    public void OpenLevelSelect()
    {
        GameObject levelPanel = GameObject.Find("LevelSelectPanel");
        if (levelPanel != null)
            levelPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject settingsPanel = GameObject.Find("SettingsPanel");
        GameObject helpPanel = GameObject.Find("HelpPanel");
        GameObject levelPanel = GameObject.Find("LevelSelectPanel");

        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (helpPanel != null) helpPanel.SetActive(false);
        if (levelPanel != null) levelPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
