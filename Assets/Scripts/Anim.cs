using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class AnimScript : MonoBehaviour
{
    [HideInInspector] public int stateInt = 1;
    [SerializeField] private Animator animator;
    
    private void Update()
    {
        if (stateInt == 1)
        {
            animator.SetInteger("State", 1);
        }
        else if (stateInt == 3)
        {
            animator.SetInteger("State", 3);
        }
        else if (stateInt == 2)
        {
            animator.SetInteger("State", 2);
        }
    }
}
