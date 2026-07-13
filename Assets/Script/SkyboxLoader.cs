using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxLoader : MonoBehaviour
{
    [Header("Skybox Materials")]
    public Material skyboxDay;
    public Material skyboxSunset;
    public Material skyboxNight;
    public Material skyboxSunrise;

    private const string SkyboxKey = "SelectedSkybox";

    void Start()
    {
        int savedIndex = PlayerPrefs.GetInt(SkyboxKey, 0); // Default Day

        switch (savedIndex)
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

        DynamicGI.UpdateEnvironment();
    }
}
