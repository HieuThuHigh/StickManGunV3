using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CamMulti : MonoBehaviour
{
    private GameObject player; // Đối tượng player mà camera sẽ theo dõi
    public float start, end, top, bot;

    void Start()
    {
        // Tìm đúng đối tượng player thuộc về client này
        foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            var photonView = obj.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                player = obj;
                break;
            }
        }

        if (player == null)
        {
            Debug.LogError("Không tìm thấy Player thuộc về client này!");
        }
    }

    void Update()
    {
        if (player == null) return; // Không có player thì không làm gì

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
