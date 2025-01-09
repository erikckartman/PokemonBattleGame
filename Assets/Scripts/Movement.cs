using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float gridSize;
    private float speed = 5f;
    public Animator animator;

    public float timeWalk;
    public float buttonHold;
    [SerializeField] private float inputX;
    [SerializeField] private float inputY;

    void Start()
    {
        timeWalk = Random.Range(0f, 10f);
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(inputX, inputY, 0) * speed * Time.deltaTime;

        if(inputX == 0 && inputY == 0)
        {
            animator.SetBool("IsGoing", false);
        }
        else
        {
            animator.SetBool("IsGoing", true);
        }

        if(inputX > 0)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }
        else if(inputX < 0)
        {
            transform.localScale = new Vector3(-4, 4, 1);
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
