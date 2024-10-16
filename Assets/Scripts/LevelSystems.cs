using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystems : MonoBehaviour
{
    public GameObject pause;
    private bool pauseAct = false;

    public static int progress = 0;
    public Text progUI;

    void Update()
    {
        pause.SetActive(pauseAct);

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            pauseAct = !pauseAct;
        }

        progUI.text = "Progress: " + progress;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
