using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; 
    public SpriteRenderer boundsSprite; 

    private float minX, maxX, minY, maxY;
    private float camHalfWidth, camHalfHeight;

    public float farY = 19f;
    public float closeY = -14f;
    public float farX = 23f;
    public float closeX = -26f;

    void Start()
    {      
        Bounds bounds = boundsSprite.bounds;
        minX = bounds.min.x + camHalfWidth;
        maxX = bounds.max.x - camHalfWidth;
        minY = bounds.min.y + camHalfHeight;
        maxY = bounds.max.y - camHalfHeight;
    }

    void LateUpdate()
    {
        Vector3 playerPos = player.position;

        float clampedX = Mathf.Clamp(playerPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(playerPos.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
