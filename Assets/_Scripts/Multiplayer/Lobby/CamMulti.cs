using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CamMulti : MonoBehaviour
{
    private GameObject player; // Đối tượng player mà camera sẽ theo dõi
    public float start, end, top, bot;

    private Vector3 lastPlayerPosition; // Lưu vị trí trước đó của player

    IEnumerator FindPlayer()
    {
        while (player == null)
        {
            foreach (var obj in FindObjectsOfType<PhotonView>())
            {
                if (obj.IsMine && obj.CompareTag("Player"))
                {
                    player = obj.gameObject;
                    lastPlayerPosition = player.transform.position;
                    yield break;
                }
            }
            yield return null; // Đợi frame tiếp theo
        }
    }

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Chưa kết nối tới Photon!");
            return;
        }

        StartCoroutine(FindPlayer());
    }


    void Update()
    {
        if (player == null) return; // Không có player thì không làm gì

        // Chỉ cập nhật vị trí camera nếu player di chuyển
        if (player.transform.position != lastPlayerPosition)
        {
            lastPlayerPosition = player.transform.position;
            UpdateCameraPosition();
        }
    }

    void UpdateCameraPosition()
    {
        var playerX = player.transform.position.x;
        var playerY = player.transform.position.y;

        // Giới hạn vị trí camera theo các giá trị start, end, top, bot
        float camX = Mathf.Clamp(playerX, start, end);
        float camY = Mathf.Clamp(playerY, bot, top);
        float camZ = transform.position.z; // Z không thay đổi

        // Cập nhật vị trí camera
        transform.position = new Vector3(camX, camY, camZ);
    }
}
