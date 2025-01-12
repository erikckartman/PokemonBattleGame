using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player; 
    public SpriteRenderer boundsSprite; 

    private float minX, maxX, minY, maxY;
    private float camHalfWidth, camHalfHeight;


    void Start()
    {      
        Bounds bounds = boundsSprite.bounds;
        float camHeight = Camera.main.orthographicSize * 2; 
        float camWidth = camHeight * Camera.main.aspect;

        minX = bounds.min.x + camWidth / 2;
        maxX = bounds.max.x - camWidth / 2;
        minY = bounds.min.y + camHeight / 2;
        maxY = bounds.max.y - camHeight / 2;
    }

    void LateUpdate()
    {
        Vector3 playerPos = player.position;

        float clampedX = Mathf.Clamp(playerPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(playerPos.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
