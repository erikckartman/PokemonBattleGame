using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystems : MonoBehaviour
{
    public Image startText;
    public GameObject selectPanel;
    public GameObject selectMode;
    public GameObject selectFile;
    public static int filenum = 0;
    
    void Start()
    {        
        StartCoroutine(StartText());
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            selectPanel.SetActive(true);
            selectMode.SetActive(true);
            selectFile.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void LoadFileSaves()
    {
        selectMode.SetActive(false);
        selectFile.SetActive(true);
    }

    public void StartNewGame()
    {
        filenum = 0;
        LevelSystems.progress = 0;
    }

    public void LoadFileOne()
    {
        filenum = 1;
    }

    public IEnumerator StartText()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            startText.enabled = !startText.enabled;
        }        
    }
}
