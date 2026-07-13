using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public AudioMixer audioMixer; // drag AudioMixer di Inspector
    public Dropdown skyboxDropdown; // 3 pilihan skybox
    public Material[] skyboxes; // assign 3 material skybox

    void Start()
    {
        if (skyboxDropdown != null)
            skyboxDropdown.onValueChanged.AddListener(SetSkybox);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSkybox(int index)
    {
        if (skyboxes != null && index < skyboxes.Length)
            RenderSettings.skybox = skyboxes[index];
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}