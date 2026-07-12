using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpeningDialog : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;

    [Header("Dialog Settings")]
    [TextArea(3, 10)]
    public string message = "Cara bermain: Untuk menggerakkan bola, gunakan tombol panah atau tombol A, W, S, D.\nUntuk pause, tekan ESC atau gunakan menu yang tersedia di layar.";
    public float typingSpeed = 0.03f;
    public float autoCloseDelay = 5f; // waktu sebelum dialog otomatis hilang

    void Start()
    {
        dialogPanel.SetActive(true);
        StartCoroutine(ShowDialog());
    }

    IEnumerator ShowDialog()
    {
        dialogText.text = "";
        foreach (char c in message)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Tunggu beberapa detik sebelum menutup panel otomatis
        yield return new WaitForSeconds(autoCloseDelay);
        dialogPanel.SetActive(false);
    }
}
