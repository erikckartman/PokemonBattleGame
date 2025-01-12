using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerSwap : MonoBehaviour
{
    [SerializeField] private GameObject[] playerList;
    [SerializeField] private CameraScript camera;
    private int playerIndex = 0;

    private void Start()
    {
        for (int i = 0; i < playerList.Length; i++)
        {
            if (i == playerIndex)
            {
                playerList[i].GetComponent<Movement>().enabled = true;
                playerList[i].gameObject.tag = "Player";
            }
            else
            {
                playerList[i].GetComponent<Movement>().enabled = false;
                playerList[i].gameObject.tag = "NPC";
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {          
            if(playerIndex < playerList.Length - 1)
            {
                playerIndex++;
            }
            else
            {
                playerIndex = 0;
            }

            camera.player = playerList[playerIndex].transform;

            for (int i = 0; i < playerList.Length; i++)
            {
                if(i  == playerIndex)
                {
                    playerList[i].GetComponent<Movement>().enabled = true;
                    playerList[i].gameObject.tag = "Player";
                }
                else
                {
                    playerList[i].GetComponent<Movement>().enabled = false;
                    playerList[i].gameObject.tag = "NPC";
                }
            }
        }
    }    
}
