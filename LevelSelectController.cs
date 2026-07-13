using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Modif2");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Modif2 - extended");
    }

    public void LoadOriginal()
    {
        SceneManager.LoadScene("minigame");
    }
}

