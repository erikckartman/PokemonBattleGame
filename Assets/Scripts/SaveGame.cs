using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.IO.Enumeration;

public class SaveGame : MonoBehaviour
{
    public GameObject player;
    [SerializeField]public SavedData data;

    private void Start()
    {
        string previousScene = PlayerPrefs.GetString("PreviousScene", "None");

        if (MenuSystems.filenum == 1 && previousScene == "Menu")
        {
            Loading();
        }

        Debug.Log(previousScene);
    }

    public void Saving()
    {
        Debug.Log("Saving...");

        data.playerPosition = player.transform.position;
        data.battleCount = LevelSystems.progress;
        SerializeManager.Save("file1", data);

        Debug.Log("Game saved");
    }

    public void Loading()
    {
        Debug.Log("Loading...");

        data = (SavedData)SerializeManager.Load("file1");
        player.transform.position = data.playerPosition;
        LevelSystems.progress = data.battleCount;

        Debug.Log("Game loaded");
    }
}
