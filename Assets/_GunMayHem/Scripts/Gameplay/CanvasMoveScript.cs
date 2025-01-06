using GameTool.Assistants.DesignPattern;
using UnityEngine;
using UnityEngine.Serialization;

public class CanvasMoveScript : SingletonMonoBehaviour<CanvasMoveScript>
{
    [FormerlySerializedAs("MoveObject")] [SerializeField] public GameObject[] moveObject; // Các MoveObject cần bật/tắt
    [SerializeField] private GameObject[] playerOn;  // Các PlayerOn được bật/tắt
    [SerializeField] private GameObject[] cameraObject;  // Các PlayerOn được bật/tắt

    private void Update()
    {
        SyncPlayerAndMoveObjects();
    }

    private void SyncPlayerAndMoveObjects()
    {
        if (playerOn.Length != moveObject.Length)
        {
            Debug.LogError("Số lượng playerOn và MoveObject không khớp!");
            return;
        }

        for (int i = 0; i < playerOn.Length; i++)
        {
            if (playerOn[i].activeSelf)
            {
                moveObject[i].SetActive(true);
                cameraObject[i].SetActive(true);
            }
            else
            {
                moveObject[i].SetActive(false);
                cameraObject[i].SetActive(false);
            }
        }
    }
}