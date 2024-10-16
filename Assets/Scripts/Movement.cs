using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float gridSize;
    public Animator animator;

    public float timeWalk;
    public float buttonHold;

    void Start()
    {
        timeWalk = Random.Range(0f, 10f);
    }

    void Update()
    {
        Vector3 newPosition = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            animator.Play("GoUp");
            transform.position += Vector3.up * gridSize;
            transform.localScale = new Vector3(3f, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.Play("GoDown");
            transform.position += Vector3.down * gridSize;
            transform.localScale = new Vector3(3f, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.Play("GoRight");
            transform.position += Vector3.left * gridSize;
            transform.localScale = new Vector3(-3f, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.Play("GoRight");
            transform.position += Vector3.right * gridSize;
            transform.localScale = new Vector3(3f, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.Play("Stay");
            transform.localScale = new Vector3(3f, transform.localScale.y, transform.localScale.z);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            animator.Play("StayRight");
            transform.localScale = new Vector3(-3f, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKeyUp(KeyCode.D)) 
        {
            animator.Play("StayRight");
            transform.localScale = new Vector3(3f, transform.localScale.y, transform.localScale.z);
        }
        else if( Input.GetKeyUp(KeyCode.W))
        {
            animator.Play("StayUp");
            transform.localScale = new Vector3(3f, transform.localScale.y, transform.localScale.z);
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            buttonHold += Time.deltaTime;
            if(buttonHold >= timeWalk)
            {
                SceneManager.LoadScene("Arena");
                buttonHold = 0;
            }
        }
        else
        {
            buttonHold = 0;
        }
    }
}
