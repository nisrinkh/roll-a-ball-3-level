using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkyboxSwitcher : MonoBehaviour
{
    [Header("UI Dropdown")]
    public TMP_Dropdown skyboxDropdown;

    [Header("Skybox Materials")]
    public Material skyboxDay;
    public Material skyboxSunset;
    public Material skyboxNight;
    public Material skyboxSunrise;

    private const string SkyboxKey = "SelectedSkybox";
    private bool initialized = false;

    void Start()
    {
        // Isi dropdown dengan nama pilihan (tidak harus sprite)
        skyboxDropdown.ClearOptions();
        skyboxDropdown.AddOptions(new System.Collections.Generic.List<string> { "Day", "Sunset", "Night", "Sunrise" });

        // Ambil pilihan terakhir dari PlayerPrefs
        int savedIndex = PlayerPrefs.GetInt(SkyboxKey, 0); // Default = 0 (Day)

        // Nonaktifkan listener sementara agar tidak memicu event ganda
        skyboxDropdown.onValueChanged.RemoveAllListeners();

        skyboxDropdown.value = savedIndex;
        skyboxDropdown.RefreshShownValue();

        // Terapkan skybox sesuai simpanan
        ApplySkybox(savedIndex);

        // Event listener
        skyboxDropdown.onValueChanged.AddListener(delegate { OnSkyboxChange(skyboxDropdown.value); });
        initialized = true;
    }

    public void OnSkyboxChange(int index)
    {
        if (!initialized) return; // cegah event awal
        ApplySkybox(index);
        PlayerPrefs.SetInt(SkyboxKey, index);
        PlayerPrefs.Save();
    }
    private void ApplySkybox(int index)
    {
        switch (index)
        {
            case 0:
                RenderSettings.skybox = skyboxDay;
                break;
            case 1:
                RenderSettings.skybox = skyboxSunset;
                break;
            case 2:
                RenderSettings.skybox = skyboxNight;
                break;
            case 3:
                RenderSettings.skybox = skyboxSunrise;
                break;
        }

        // Terapkan perubahan agar langsung terlihat
        DynamicGI.UpdateEnvironment();
    }
}
