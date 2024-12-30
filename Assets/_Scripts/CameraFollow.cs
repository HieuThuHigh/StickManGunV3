using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float start, end, top, bot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var playerX = player.transform.position.x;
        var playerY = player.transform.position.y;
        var camX = transform.position.x;
        var camY = transform.position.y;
        var camZ = transform.position.z;
        if (playerX > start && playerX < end)
        {
            camX = playerX;
        }
        else
        {
            if (playerX < start)
            {
                camX = start;
            }
            if (playerX > end)
            {
                camX = end;
            }
        }
        if (playerY > bot && playerY < top)
        {
            camY = playerY;
        }
        else
        {
            if (playerY < bot)
            {
                camY = bot;
            }
            if (playerY > top)
            {
                camY = top;
            }
        }
        transform.position = new Vector3(camX, camY, camZ);
    }
}