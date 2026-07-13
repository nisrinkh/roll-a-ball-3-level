using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("Volume Slider")]
    public Slider volumeSlider;

    private const string VolumeKey = "GameVolume";

    void Start()
    {
        // Ambil nilai volume yang tersimpan, default 1 (penuh)
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        volumeSlider.value = savedVolume;

        // Atur volume audio utama
        AudioListener.volume = savedVolume;

        // Tambahkan listener saat slider digeser
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    public void OnVolumeChange(float value)
    {
        // Ubah volume utama
        AudioListener.volume = value;

        // Simpan preferensi
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
}
