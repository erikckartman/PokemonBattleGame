using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRandom : MonoBehaviour
{
    private int number;

    public AudioSource kse;
    public AudioSource aild;
    public AudioSource inflames;
    public AudioSource trivium;
    public AudioSource log;
    void Start()
    {
        number = Random.Range(1, 6);

        switch (number)
        {
            case 1:
                kse.Play(); 
                break;
            case 2:
                aild.Play();
                break;
            case 3: 
                inflames.Play();
                break;
            case 4:
                log.Play();
                break;
            case 5:
                trivium.Play();
                break;
        }
    }    
}
