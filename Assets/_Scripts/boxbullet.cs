using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Thêm Photon PUN

public class boxbullet : MonoBehaviourPun
{
    public GameObject boxPrefab; // Prefab của hộp
    public float spawnInterval = 20f; // Thời gian giữa các lần xuất hiện
    public float raycastDistance = 10f; // Khoảng cách raycast từ trên xuống

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient) // Chỉ MasterClient mới thực hiện spawn
        {
            StartCoroutine(SpawnBox());
        }
    }

    private IEnumerator SpawnBox()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnBoxOnGround();
        }
    }

    private void SpawnBoxOnGround()
    {
        // Tìm tất cả các đối tượng trong Scene có Layer là "Ground"
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");

        if (groundObjects.Length == 0) return;

        // Chọn ngẫu nhiên một Ground Object
        int randomIndex = Random.Range(0, groundObjects.Length);
        GameObject selectedGround = groundObjects[randomIndex];

        // Lấy kích thước của Ground để xác định vị trí spawn
        Collider2D groundCollider = selectedGround.GetComponent<Collider2D>();
        if (groundCollider != null)
        {
            // Xác định điểm xuất hiện ở trên Ground Object
            Vector2 spawnPosition = new Vector2(Random.Range(groundCollider.bounds.min.x, groundCollider.bounds.max.x), groundCollider.bounds.max.y + 1);

            // Gọi RPC để tạo hộp trên tất cả các client
            photonView.RPC("RPC_SpawnBox", RpcTarget.All, spawnPosition);
        }
    }

    [PunRPC]
    private void RPC_SpawnBox(Vector2 spawnPosition)
    {
        // Tạo hộp tại vị trí spawn
        Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
    }
}
