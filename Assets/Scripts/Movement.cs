using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    private float size;

    public SpriteRenderer boundsSprite;
    private float minX, maxX, minY, maxY;

    private void Start()
    {
        size = transform.localScale.x;

        if (boundsSprite != null)
        {
            Bounds bounds = boundsSprite.bounds;

            minX = bounds.min.x;
            maxX = bounds.max.x;
            minY = bounds.min.y;
            maxY = bounds.max.y;
        }
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

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
            transform.localScale = new Vector3(size, size, 1);
        }
        else if(inputX < 0)
        {
            transform.localScale = new Vector3(-size, size, 1);
        }

        
    }

    private void LateUpdate()
    {
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
